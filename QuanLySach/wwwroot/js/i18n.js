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
        copyright: "© 2026 Book Paradise. All rights reserved."
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
        copyright: "© 2026 Thiên đường sách. Bảo lưu mọi quyền."
    }
};

const langLabels = { ru: '🇷🇺 RU', en: '🇬🇧 EN', vi: '🇻🇳 VI' };

function applyLang(lang) {
    document.querySelectorAll('[data-i18n]').forEach(el => {
        // Lưu lại chữ tiếng Nga gốc lần đầu tiên (chỉ lưu 1 lần)
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

function setLang(lang) {
    localStorage.setItem('siteLang', lang);
    applyLang(lang);
}

// Tự áp dụng ngôn ngữ đã lưu (nếu có) khi tải trang
document.addEventListener('DOMContentLoaded', function () {
    const saved = localStorage.getItem('siteLang');
    if (saved && saved !== 'ru') {
        applyLang(saved);
    }
});