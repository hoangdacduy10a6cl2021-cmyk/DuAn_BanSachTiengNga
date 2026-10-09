// ===== Hệ thống dịch tĩnh (không phụ thuộc dịch vụ ngoài) =====
const translations = {
    en: {
        shipping_info: "Shipping & Payment",
        about_us: "About us",
        contact: "Contacts",
        tagline: "— Book Paradise —",
        search_placeholder: "Search books...",
        login: "Login",
        account: "Account",
        cart: "Cart",
        home: "Home",
        books: "Books",
        all_books: "All books",
        new_arrivals: "New arrivals",
        popular: "Popular",
        promotions: "Promotions",
        feature1_title: "Wide assortment",
        feature1_desc: "Thousands of books for every taste",
        feature2_title: "Fast delivery",
        feature2_desc: "Delivery across Russia",
        feature3_title: "Quality guarantee",
        feature3_desc: "Only original books",
        feature4_title: "Discounts & promotions",
        feature4_desc: "Great deals",
        catalog: "Catalog",
        categories: "Categories",
        for_buyers: "For buyers",
        returns: "Returns",
        help: "Help",
        address: "6/3 Myasnitskaya St., Bldg. 1, Moscow, Russia, 101000",
        copyright: "© 2026 Book Paradise. All rights reserved.",
        added_to_cart: "Book added to cart!",
        fill_required_fields: "Please fill in all required fields!",
        not_found: "Not found"
    },
    vi: {
        shipping_info: "Giao hàng & Thanh toán",
        about_us: "Về chúng tôi",
        contact: "Liên hệ",
        tagline: "— Thiên đường sách —",
        search_placeholder: "Tìm kiếm sách...",
        login: "Đăng nhập",
        account: "Tài khoản",
        cart: "Giỏ hàng",
        home: "Trang chủ",
        books: "Sách",
        all_books: "Tất cả sách",
        new_arrivals: "Sách mới",
        popular: "Phổ biến",
        promotions: "Khuyến mãi",
        feature1_title: "Đa dạng sản phẩm",
        feature1_desc: "Hàng ngàn cuốn sách cho mọi sở thích",
        feature2_title: "Giao hàng nhanh",
        feature2_desc: "Giao hàng trên toàn nước Nga",
        feature3_title: "Đảm bảo chất lượng",
        feature3_desc: "Chỉ bán sách chính hãng",
        feature4_title: "Giảm giá & khuyến mãi",
        feature4_desc: "Ưu đãi hấp dẫn",
        catalog: "Danh mục",
        categories: "Thể loại",
        for_buyers: "Dành cho khách hàng",
        returns: "Đổi trả",
        help: "Trợ giúp",
        address: "Phố Myasnitskaya, số 6/3, tòa 1, Moscow, Nga, 101000",
        copyright: "© 2026 Thiên đường sách. Bảo lưu mọi quyền.",
        added_to_cart: "Đã thêm sách vào giỏ hàng!",
        fill_required_fields: "Vui lòng điền đầy đủ tất cả các trường bắt buộc!",
        not_found: "Không tìm thấy"
    }
};

const langLabels = { ru: '🇷🇺 RU', en: '🇬🇧 EN', vi: '🇻🇳 VI' };

function applyLang(lang) {
    document.querySelectorAll('[data-i18n]').forEach(el => {
        if (!el.dataset.i18nRu) {
            el.dataset.i18nRu = el.textContent;
        }
        const key = el.dataset.i18n;
        if (lang === 'ru') {
            el.textContent = el.dataset.i18nRu;
        } else if (translations[lang] && translations[lang][key]) {
            el.textContent = translations[lang][key];
        }
    });

    document.querySelectorAll('[data-i18n-placeholder]').forEach(el => {
        if (!el.dataset.i18nPlaceholderRu) {
            el.dataset.i18nPlaceholderRu = el.getAttribute('placeholder') || '';
        }
        const key = el.dataset.i18nPlaceholder;
        if (lang === 'ru') {
            el.setAttribute('placeholder', el.dataset.i18nPlaceholderRu);
        } else if (translations[lang] && translations[lang][key]) {
            el.setAttribute('placeholder', translations[lang][key]);
        }
    });

    const label = document.getElementById('lang-current-label');
    if (label) {
        label.innerHTML = '<i class="fas fa-globe"></i> ' + langLabels[lang] + ' ▾';
    }
}

// ===== Tự động dịch phần còn lại của trang (không có data-i18n) =====
const AUTO_CACHE_KEY = 'i18n_auto_cache_v3';

function loadAutoCache() {
    try {
        return JSON.parse(localStorage.getItem(AUTO_CACHE_KEY) || '{"en":{},"vi":{}}');
    } catch {
        return { en: {}, vi: {} };
    }
}

function saveAutoCache(cache) {
    try {
        localStorage.setItem(AUTO_CACHE_KEY, JSON.stringify(cache));
    } catch {
        // localStorage đầy hoặc bị chặn -> bỏ qua
    }
}

const AUTO_SKIP_TAGS = new Set(['SCRIPT', 'STYLE', 'NOSCRIPT', 'CODE', 'PRE', 'SVG']);

const originalTextNodeMap = new Map();

function isSkippableNode(node) {
    let el = node.nodeType === 3 ? node.parentElement : node;
    while (el) {
        if (AUTO_SKIP_TAGS.has(el.tagName)) return true;
        if (el.classList && el.classList.contains('notranslate')) return true;
        if (el.hasAttribute && (el.hasAttribute('data-i18n') || el.hasAttribute('data-i18n-placeholder'))) return true;
        el = el.parentElement;
    }
    return false;
}

function collectTextNodes(root) {
    const walker = document.createTreeWalker(root.body, NodeFilter.SHOW_TEXT, null);
    const nodes = [];
    let node;
    while ((node = walker.nextNode())) {
        const text = node.nodeValue.trim();
        if (!text || text.length > 400) continue;
        // Bỏ qua các đoạn CHỈ chứa số + ký hiệu tiền/số lượng (vd: "550,00 ₽ × 1").
        // Trước đây thiếu ký hiệu ₽ và × nên Google Translate lỡ dịch luôn các dòng
        // giá tiền, biến "₽" thành chữ "RUR" trong phần thân trang.
        if (/^[\d\s.,%+\-:/()₫$€₽×шт]*$/.test(text)) continue;
        if (isSkippableNode(node)) continue;
        nodes.push(node);
    }
    return nodes;
}

function collectAttrElements(root) {
    const result = [];
    root.querySelectorAll('[placeholder]:not([data-i18n-placeholder])').forEach(el => result.push({ el, attr: 'placeholder' }));
    root.querySelectorAll('[title]').forEach(el => result.push({ el, attr: 'title' }));
    root.querySelectorAll('input[type="submit"], input[type="button"]').forEach(el => result.push({ el, attr: 'value' }));
    return result;
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

async function translateOne(text, targetLang) {
    const maxAttempts = 3;
    for (let attempt = 1; attempt <= maxAttempts; attempt++) {
        try {
            const url = 'https://translate.googleapis.com/translate_a/single'
                + '?client=gtx&sl=ru&tl=' + targetLang + '&dt=t&q=' + encodeURIComponent(text);
            const resp = await fetch(url);
            if (!resp.ok) throw new Error('HTTP ' + resp.status);
            const data = await resp.json();
            const translated = (data[0] || []).map(seg => seg[0]).join('');
            if (translated) return { text: translated, ok: true };
        } catch (e) {
            // rơi xuống retry bên dưới
        }
        if (attempt < maxAttempts) await sleep(250 * attempt);
    }
    return { text, ok: false };
}

async function callTranslateBatch(texts, targetLang) {
    const results = new Array(texts.length);
    const ok = new Array(texts.length);
    // Google hay chặn/429 nếu bắn quá nhiều request song song cùng lúc từ 1 trình duyệt,
    // đặc biệt các trang có nhiều chữ (Profile, Checkout...). Giảm còn 2 luồng song song
    // + có retry (translateOne) để tăng tỉ lệ dịch thành công, tránh bị dịch dở dang.
    const CONCURRENCY = 2;
    let idx = 0;

    async function worker() {
        while (idx < texts.length) {
            const i = idx++;
            const { text: translated, ok: success } = await translateOne(texts[i], targetLang);
            results[i] = translated;
            ok[i] = success;
            // Giãn nhẹ giữa các request để giảm khả năng bị rate-limit.
            await sleep(60);
        }
    }

    await Promise.all(Array.from({ length: CONCURRENCY }, worker));
    return { results, ok };
}

async function autoTranslatePage(lang) {
    if (lang === 'ru') {
        restoreOriginalText();
        return;
    }

    const cache = loadAutoCache();
    const langCache = cache[lang] || (cache[lang] = {});

    const textNodes = collectTextNodes(document);
    const attrEntries = collectAttrElements(document);

    textNodes.forEach(node => {
        if (!originalTextNodeMap.has(node)) originalTextNodeMap.set(node, node.nodeValue);
    });
    attrEntries.forEach(({ el, attr }) => {
        const key = 'i18nAutoRu_' + attr;
        if (!el.dataset[key]) el.dataset[key] = el.getAttribute(attr) || '';
    });

    const pending = new Set();
    textNodes.forEach(node => {
        const original = originalTextNodeMap.get(node).trim();
        if (original && !(original in langCache)) pending.add(original);
    });
    attrEntries.forEach(({ el, attr }) => {
        const original = el.dataset['i18nAutoRu_' + attr];
        if (original && original.trim() && !(original.trim() in langCache)) pending.add(original.trim());
    });

    if (pending.size > 0) {
        try {
            const texts = Array.from(pending);
            const { results: translated, ok } = await callTranslateBatch(texts, lang);
            texts.forEach((t, i) => {
                // Chỉ lưu vào cache khi server xác nhận dịch THÀNH CÔNG.
                // Nếu dịch thất bại (bị chặn/rate-limit), KHÔNG cache để lần đổi
                // ngôn ngữ tiếp theo sẽ tự thử dịch lại, thay vì kẹt tiếng Nga vĩnh viễn.
                if (ok && ok[i]) {
                    langCache[t] = translated[i] ?? t;
                }
            });
            saveAutoCache(cache);
        } catch (e) {
            console.warn('Không dịch được (kiểm tra kết nối mạng / endpoint dịch):', e);
        }
    }

    textNodes.forEach(node => {
        const original = originalTextNodeMap.get(node).trim();
        if (original && langCache[original]) node.nodeValue = langCache[original];
    });
    attrEntries.forEach(({ el, attr }) => {
        const original = (el.dataset['i18nAutoRu_' + attr] || '').trim();
        if (original && langCache[original]) el.setAttribute(attr, langCache[original]);
    });
}

function restoreOriginalText() {
    originalTextNodeMap.forEach((originalText, node) => {
        node.nodeValue = originalText;
    });
    document.querySelectorAll('*').forEach(el => {
        ['placeholder', 'title', 'value'].forEach(attr => {
            const key = 'i18nAutoRu_' + attr;
            if (el.dataset[key] !== undefined) el.setAttribute(attr, el.dataset[key]);
        });
    });
}

// ===== Dịch văn bản tạo động bằng JS (thông báo từ server, nhãn biểu đồ Chart.js...) =====
// Chữ vẽ trong <canvas> và chuỗi sinh ra sau khi trang tải xong KHÔNG nằm trong DOM lúc quét,
// nên autoTranslatePage() không dịch được. Các hàm dưới đây xử lý riêng cho những trường hợp đó.
// Quy ước: mọi chuỗi gốc luôn viết bằng TIẾNG NGA, hàm tự đổi sang ngôn ngữ đang chọn.

// Bảng dịch cố định (chính xác hơn Google cho từ ngắn như tên tháng, trạng thái đơn hàng)
const STATIC_RU_TEXT = {
    vi: {
        'Янв': 'Th1', 'Фев': 'Th2', 'Мар': 'Th3', 'Апр': 'Th4', 'Май': 'Th5', 'Июн': 'Th6',
        'Июл': 'Th7', 'Авг': 'Th8', 'Сен': 'Th9', 'Окт': 'Th10', 'Ноя': 'Th11', 'Дек': 'Th12',
        'Доход (₽)': 'Thu nhập (₽)',
        'Новый': 'Mới',
        'Обрабатывается': 'Đang xử lý',
        'Доставляется': 'Đang giao',
        'Выполнен': 'Hoàn thành',
        'Отменён': 'Đã hủy',
        'Прочее': 'Khác'
    },
    en: {
        'Янв': 'Jan', 'Фев': 'Feb', 'Мар': 'Mar', 'Апр': 'Apr', 'Май': 'May', 'Июн': 'Jun',
        'Июл': 'Jul', 'Авг': 'Aug', 'Сен': 'Sep', 'Окт': 'Oct', 'Ноя': 'Nov', 'Дек': 'Dec',
        'Доход (₽)': 'Revenue (₽)',
        'Новый': 'New',
        'Обрабатывается': 'Processing',
        'Доставляется': 'Shipping',
        'Выполнен': 'Completed',
        'Отменён': 'Cancelled',
        'Прочее': 'Other'
    }
};

function getSiteLang() {
    return localStorage.getItem('siteLang') || 'ru';
}

// Dịch 1 mảng chuỗi tiếng Nga -> ngôn ngữ lang. Thứ tự tra: bảng cố định -> cache -> Google.
// Chuỗi chỉ gồm số/ký hiệu (vd "27/08") được giữ nguyên.
async function translateRuTexts(texts, lang) {
    if (lang === 'ru') return texts.slice();

    const staticMap = STATIC_RU_TEXT[lang] || {};
    const cache = loadAutoCache();
    const langCache = cache[lang] || (cache[lang] = {});
    const out = texts.slice();
    const pendingIdx = [];

    texts.forEach((raw, i) => {
        const key = (raw == null ? '' : String(raw)).trim();
        if (!key || /^[\d\s.,%+\-:/()₫$€₽×шт]*$/.test(key)) return;
        if (staticMap[key]) { out[i] = staticMap[key]; return; }
        if (langCache[key]) { out[i] = langCache[key]; return; }
        pendingIdx.push(i);
    });

    if (pendingIdx.length > 0) {
        const uniq = Array.from(new Set(pendingIdx.map(i => String(texts[i]).trim())));
        const { results, ok } = await callTranslateBatch(uniq, lang);
        uniq.forEach((t, j) => {
            if (ok[j]) langCache[t] = results[j] ?? t;
        });
        saveAutoCache(cache);
        pendingIdx.forEach(i => {
            const key = String(texts[i]).trim();
            if (langCache[key]) out[i] = langCache[key];
        });
    }
    return out;
}

// Dịch 1 chuỗi tiếng Nga sang ngôn ngữ đang chọn (dùng cho thông báo tạo động).
async function translateRuText(text) {
    const [result] = await translateRuTexts([text], getSiteLang());
    return result;
}

// ----- Biểu đồ Chart.js -----
const translatableCharts = [];
let chartRefreshToken = 0;

// Gọi 1 lần sau khi tạo biểu đồ:  const c = new Chart(...); registerTranslatableChart(c);
// Nhãn trục / legend / tên dataset gốc (tiếng Nga) được lưu lại để dịch qua lại RU <-> EN <-> VI.
function registerTranslatableChart(chart) {
    chart.$ruLabels = Array.isArray(chart.data.labels) ? chart.data.labels.slice() : [];
    chart.$ruDatasetLabels = chart.data.datasets.map(d => d.label);
    translatableCharts.push(chart);
    refreshTranslatableCharts();
}

async function refreshTranslatableCharts() {
    const token = ++chartRefreshToken;
    const lang = getSiteLang();

    for (const chart of translatableCharts) {
        try {
            let labels = chart.$ruLabels.slice();
            let datasetLabels = chart.$ruDatasetLabels.slice();

            if (lang !== 'ru') {
                labels = await translateRuTexts(chart.$ruLabels, lang);
                datasetLabels = await translateRuTexts(chart.$ruDatasetLabels.map(l => l || ''), lang);
            }

            // Người dùng vừa đổi ngôn ngữ lần nữa -> bỏ kết quả cũ
            if (token !== chartRefreshToken) return;

            chart.data.labels = labels;
            chart.data.datasets.forEach((d, i) => {
                if (chart.$ruDatasetLabels[i]) d.label = datasetLabels[i];
            });
            chart.update();
        } catch (e) {
            console.warn('Không dịch được nhãn biểu đồ:', e);
        }
    }
}

function setLang(lang) {
    localStorage.setItem('siteLang', lang);
    applyLang(lang);
    autoTranslatePage(lang);
    refreshTranslatableCharts();
}

// Dùng cho các thông báo được tạo động bằng JS (toast, alert...) sau khi trang đã tải xong,
// vì những nội dung này sinh ra SAU lúc quét dịch tự động nên không được xử lý cùng.
function t(key, fallbackRu) {
    const lang = localStorage.getItem('siteLang') || 'ru';
    if (lang === 'ru') return fallbackRu;
    return (translations[lang] && translations[lang][key]) || fallbackRu;
}

document.addEventListener('DOMContentLoaded', function () {
    const saved = localStorage.getItem('siteLang');
    if (saved && saved !== 'ru') {
        applyLang(saved);
        autoTranslatePage(saved);
    }
});