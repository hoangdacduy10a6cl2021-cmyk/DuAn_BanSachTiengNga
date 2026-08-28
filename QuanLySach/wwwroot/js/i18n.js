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
        fill_required_fields: "Please fill in all required fields!"
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
        fill_required_fields: "Vui lòng điền đầy đủ tất cả các trường bắt buộc!"
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
        if (/^[\d\s.,%+\-:/()₫$€]*$/.test(text)) continue;
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

async function callTranslateBatch(texts, targetLang) {
    const resp = await fetch('/Translate/Batch', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ texts, targetLang })
    });
    if (!resp.ok) throw new Error('Translate API error: ' + resp.status);
    const data = await resp.json();
    return { results: data.results || texts, ok: data.ok || texts.map(() => false) };
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

function setLang(lang) {
    localStorage.setItem('siteLang', lang);
    applyLang(lang);
    autoTranslatePage(lang);
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