// =====================================================================
// admin-mobile.js - hành vi mobile cho khu vực Admin
//  1. Mở/đóng sidebar (nút ☰, bấm nền tối, phím Esc)
//  2. Bọc mọi bảng .admin-table vào .table-scroll để cuộn ngang
//  3. Menu chọn ngôn ngữ mở bằng cách chạm (mobile không có hover)
// Việc DỊCH vẫn do i18n.js (setLang / autoTranslatePage) xử lý y như bản web.
// =====================================================================
(function () {
    'use strict';

    var root = document.documentElement;
    var body = document.body;

    function isMobile() {
        return root.classList.contains('is-mobile');
    }

    // ---------- 1. Sidebar ----------
    var toggleBtn = document.getElementById('sidebar-toggle');
    var overlay = document.getElementById('sidebar-overlay');

    function setSidebar(open) {
        body.classList.toggle('sidebar-open', open);
        if (toggleBtn) toggleBtn.setAttribute('aria-expanded', open ? 'true' : 'false');
    }

    if (toggleBtn) {
        toggleBtn.addEventListener('click', function () {
            setSidebar(!body.classList.contains('sidebar-open'));
        });
    }
    if (overlay) {
        overlay.addEventListener('click', function () { setSidebar(false); });
    }
    window.addEventListener('resize', function () {
        if (!isMobile()) setSidebar(false);
    });

    // ---------- 2. Bọc bảng để cuộn ngang ----------
    document.querySelectorAll('table.admin-table').forEach(function (table) {
        var cols = table.querySelectorAll('thead th').length;
        if (cols) table.style.setProperty('--cols', cols);

        if (table.parentElement && table.parentElement.classList.contains('table-scroll')) return;
        var wrapper = document.createElement('div');
        wrapper.className = 'table-scroll';
        table.parentNode.insertBefore(wrapper, table);
        wrapper.appendChild(table);
    });

    // ---------- 3. Menu ngôn ngữ ----------
    var langBox = document.querySelector('.lang-selector');

    function closeLang() {
        if (langBox) langBox.classList.remove('open');
    }

    if (langBox) {
        var current = langBox.querySelector('.lang-current');
        if (current) {
            current.addEventListener('click', function (e) {
                if (!isMobile()) return;          // desktop vẫn dùng hover như cũ
                e.stopPropagation();
                langBox.classList.toggle('open');
            });
        }
        // onclick="setLang(..)" trong HTML chạy trước, sau đó mới đóng menu
        langBox.querySelectorAll('.lang-dropdown li').forEach(function (li) {
            li.addEventListener('click', function () {
                closeLang();
                setSidebar(false);
            });
        });
        document.addEventListener('click', function (e) {
            if (!langBox.contains(e.target)) closeLang();
        });
    }

    document.addEventListener('keydown', function (e) {
        if (e.key === 'Escape') {
            setSidebar(false);
            closeLang();
        }
    });
})();