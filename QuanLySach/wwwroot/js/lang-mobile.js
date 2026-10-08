// =====================================================================
// lang-mobile.js - nút chọn ngôn ngữ trên mobile (web bán sách)
// Dịch vẫn do i18n.js xử lý (setLang -> applyLang + autoTranslatePage),
// file này chỉ lo: chạm để mở/đóng menu + cập nhật nhãn "🇷🇺 RU ▾".
// Nạp SAU i18n.js.
// =====================================================================
(function () {
    'use strict';

    var box = document.querySelector('.lang-selector-mobile');
    if (!box) return;

    var current = box.querySelector('.lang-current');

    function updateLabel(lang) {
        var text = (typeof langLabels !== 'undefined' && langLabels[lang]) ? langLabels[lang] : String(lang).toUpperCase();
        current.innerHTML = '<i class="fas fa-globe"></i> ' + text + ' ▾';
    }

    // Mỗi lần i18n.js gọi applyLang(lang) thì cập nhật luôn nhãn của nút mobile
    if (typeof window.applyLang === 'function') {
        var originalApplyLang = window.applyLang;
        window.applyLang = function (lang) {
            originalApplyLang.apply(this, arguments);
            updateLabel(lang);
        };
    }
    updateLabel(localStorage.getItem('siteLang') || 'ru');

    function close() { box.classList.remove('open'); }

    current.addEventListener('click', function (e) {
        e.stopPropagation();
        box.classList.toggle('open');
    });

    // onclick="setLang(..)" trong HTML chạy trước, rồi mới đóng menu
    box.querySelectorAll('.lang-dropdown li').forEach(function (li) {
        li.addEventListener('click', close);
    });

    document.addEventListener('click', function (e) {
        if (!box.contains(e.target)) close();
    });
    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape') close();
    });
})();