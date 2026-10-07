// =====================================================================
// Dịch từ khoá tìm kiếm sang TIẾNG NGA trước khi gửi lên server.
//
// Vì sao cần: DB lưu tên sách và tên tác giả bằng tiếng Nga. Phần "English / Tiếng Việt"
// của site chỉ là dịch hiển thị bằng trình duyệt, nên gõ "Hoàng tử bé" hay "Harry Potter"
// thì câu lệnh LIKE trong SQL không khớp với "Маленький принц" / "Гарри Поттер".
//
// Cách làm: gọi Google Translate từ trình duyệt (giống cách i18n.js đang dịch trang),
// tự nhận diện ngôn ngữ nguồn (sl=auto), đích là tiếng Nga (tl=ru).
// Nếu lỗi mạng / bị chặn thì trả về đúng từ khoá người dùng gõ => hành vi như cũ.
//
// Dùng: const q = await toRussianQuery('Hoàng tử bé');   // "Маленький принц"
// =====================================================================
(function () {
    const CYRILLIC = /[\u0400-\u04FF]/;
    const HAS_LETTER = /\p{L}/u;
    const TIMEOUT_MS = 3000;
    const cache = new Map();   // chỉ cache khi dịch THÀNH CÔNG

    async function toRussianQuery(query) {
        const q = (query || '').trim();
        if (!q) return q;

        // Đã có chữ Nga -> người dùng gõ tiếng Nga rồi, không cần dịch
        if (CYRILLIC.test(q)) return q;

        // Chỉ có số / ký hiệu (ví dụ "1984") -> không cần dịch
        if (!HAS_LETTER.test(q)) return q;

        const key = q.toLowerCase();
        if (cache.has(key)) return cache.get(key);

        const controller = new AbortController();
        const timer = setTimeout(() => controller.abort(), TIMEOUT_MS);

        try {
            const url = 'https://translate.googleapis.com/translate_a/single'
                + '?client=gtx&sl=auto&tl=ru&dt=t&q=' + encodeURIComponent(q);
            const resp = await fetch(url, { signal: controller.signal });
            if (!resp.ok) throw new Error('HTTP ' + resp.status);

            const data = await resp.json();
            const translated = (data[0] || []).map(seg => seg[0]).join('').trim();

            if (!translated) return q;

            cache.set(key, translated);
            return translated;
        } catch (e) {
            return q;   // lỗi mạng / timeout / bị chặn: giữ nguyên từ khoá gốc
        } finally {
            clearTimeout(timer);
        }
    }

    window.toRussianQuery = toRussianQuery;
})();