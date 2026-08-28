using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace QuanLySach.Controllers
{
    public class TranslateController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _cache;

        public TranslateController(IHttpClientFactory httpClientFactory, IMemoryCache cache)
        {
            _httpClientFactory = httpClientFactory;
            _cache = cache;
        }

        public class BatchRequest
        {
            public List<string> Texts { get; set; } = new();
            public string TargetLang { get; set; } = "en";
        }

        // POST /Translate/Batch
        // Dùng Google Translate (endpoint miễn phí, không cần API key) qua server để tránh lỗi CORS.
        [HttpPost]
        public async Task<IActionResult> Batch([FromBody] BatchRequest request)
        {
            if (request == null || request.Texts == null || request.Texts.Count == 0)
                return Json(new { results = new List<string>(), ok = new List<bool>() });

            var targetLang = request.TargetLang == "vi" ? "vi" : "en";
            var client = _httpClientFactory.CreateClient();
            var results = new List<string>(new string[request.Texts.Count]);
            var okFlags = new List<bool>(new bool[request.Texts.Count]);

            // Giảm số request đồng thời để tránh bị Google rate-limit (429) khi trang có nhiều chữ cần dịch.
            var semaphore = new SemaphoreSlim(4);
            var tasks = new List<Task>();

            for (int i = 0; i < request.Texts.Count; i++)
            {
                int index = i;
                string text = request.Texts[i] ?? string.Empty;
                var cacheKey = $"tr:{targetLang}:{text}";

                if (_cache.TryGetValue(cacheKey, out string? cached))
                {
                    results[index] = cached!;
                    okFlags[index] = true;
                    continue;
                }

                tasks.Add(Task.Run(async () =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        var (translated, ok) = await TranslateOneAsync(client, text, targetLang);
                        results[index] = translated;
                        okFlags[index] = ok;
                        // QUAN TRỌNG: chỉ cache khi dịch THÀNH CÔNG.
                        // Trước đây cache cả kết quả thất bại (= text gốc) khiến câu đó
                        // bị kẹt tiếng Nga vĩnh viễn trong 30 ngày, không bao giờ thử lại.
                        if (ok)
                        {
                            _cache.Set(cacheKey, translated, TimeSpan.FromDays(30));
                        }
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }));
            }

            await Task.WhenAll(tasks);

            return Json(new { results, ok = okFlags });
        }

        private async Task<(string text, bool ok)> TranslateOneAsync(HttpClient client, string text, string targetLang)
        {
            if (string.IsNullOrWhiteSpace(text)) return (text, true);

            const int maxAttempts = 3;
            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    var url = "https://translate.googleapis.com/translate_a/single"
                        + "?client=gtx&sl=ru&tl=" + targetLang + "&dt=t&q=" + Uri.EscapeDataString(text);

                    using var reqMsg = new HttpRequestMessage(HttpMethod.Get, url);
                    // Google có thể chặn/giới hạn request không có User-Agent giống trình duyệt thật.
                    reqMsg.Headers.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36");

                    var resp = await client.SendAsync(reqMsg);

                    if (!resp.IsSuccessStatusCode)
                    {
                        if (attempt < maxAttempts)
                        {
                            await Task.Delay(200 * attempt);
                            continue;
                        }
                        return (text, false);
                    }

                    var body = await resp.Content.ReadAsStringAsync();
                    using var doc = JsonDocument.Parse(body);

                    var sb = new StringBuilder();
                    foreach (var segment in doc.RootElement[0].EnumerateArray())
                    {
                        var piece = segment[0].GetString();
                        if (piece != null) sb.Append(piece);
                    }
                    var result = sb.ToString();

                    if (string.IsNullOrWhiteSpace(result))
                    {
                        if (attempt < maxAttempts)
                        {
                            await Task.Delay(200 * attempt);
                            continue;
                        }
                        return (text, false);
                    }

                    return (result, true);
                }
                catch
                {
                    if (attempt < maxAttempts)
                    {
                        await Task.Delay(200 * attempt);
                        continue;
                    }
                    return (text, false);
                }
            }

            return (text, false);
        }
    }
}