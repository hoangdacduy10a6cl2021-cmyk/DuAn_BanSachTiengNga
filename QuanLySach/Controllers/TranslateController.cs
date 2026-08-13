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
                return Json(new { results = new List<string>() });

            var targetLang = request.TargetLang == "vi" ? "vi" : "en";
            var client = _httpClientFactory.CreateClient();
            var results = new List<string>(new string[request.Texts.Count]);

            var semaphore = new SemaphoreSlim(8);
            var tasks = new List<Task>();

            for (int i = 0; i < request.Texts.Count; i++)
            {
                int index = i;
                string text = request.Texts[i] ?? string.Empty;
                var cacheKey = $"tr:{targetLang}:{text}";

                if (_cache.TryGetValue(cacheKey, out string? cached))
                {
                    results[index] = cached!;
                    continue;
                }

                tasks.Add(Task.Run(async () =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        var translated = await TranslateOneAsync(client, text, targetLang);
                        results[index] = translated;
                        _cache.Set(cacheKey, translated, TimeSpan.FromDays(30));
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }));
            }

            await Task.WhenAll(tasks);

            return Json(new { results });
        }

        private async Task<string> TranslateOneAsync(HttpClient client, string text, string targetLang)
        {
            if (string.IsNullOrWhiteSpace(text)) return text;

            try
            {
                var url = "https://translate.googleapis.com/translate_a/single"
                    + "?client=gtx&sl=ru&tl=" + targetLang + "&dt=t&q=" + Uri.EscapeDataString(text);

                var resp = await client.GetAsync(url);
                if (!resp.IsSuccessStatusCode) return text;

                var body = await resp.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(body);

                var sb = new StringBuilder();
                foreach (var segment in doc.RootElement[0].EnumerateArray())
                {
                    var piece = segment[0].GetString();
                    if (piece != null) sb.Append(piece);
                }
                var result = sb.ToString();
                return string.IsNullOrWhiteSpace(result) ? text : result;
            }
            catch
            {
                return text;
            }
        }
    }
}