// Thêm vào giỏ hàng bằng AJAX
function addToCart(bookId) {
    fetch('/Cart/AddToCart', {
        method: 'POST',
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
        body: 'bookId=' + bookId
    })
        .then(res => res.json())
        .then(data => {
            if (data.success) {
                showNotification('Книга добавлена в корзину!');
            }
        });
}

function getToken() {
    return document.querySelector('input[name="__RequestVerificationToken"]')?.value ?? '';
}

function showNotification(msg) {
    const div = document.createElement('div');
    div.textContent = msg;
    div.style.cssText = 'position:fixed;bottom:20px;right:20px;background:#b8860b;color:#fff;padding:12px 20px;border-radius:6px;z-index:9999;font-size:14px;';
    document.body.appendChild(div);
    setTimeout(() => div.remove(), 3000);
}

// SLIDESHOW
let currentSlide = 0;
const slides = document.querySelectorAll('.hero-slide');
const dots = document.querySelectorAll('.hero-dots .dot');

function goToSlide(n) {
    if (slides.length === 0) return;
    slides[currentSlide].classList.remove('active');
    dots[currentSlide].classList.remove('active');
    currentSlide = (n + slides.length) % slides.length;
    slides[currentSlide].classList.add('active');
    dots[currentSlide].classList.add('active');
}

function changeSlide(direction) {
    goToSlide(currentSlide + direction);
}

if (slides.length > 0) {
    setInterval(() => changeSlide(1), 4000);
}

// TRANSLATE
function translatePage(lang) {
    const langNames = { 'en': 'EN', 'vi': 'VI', 'de': 'DE', 'ru': 'RU' };
    const langCurrent = document.querySelector('.lang-current');
    if (langCurrent) {
        langCurrent.innerHTML = '<i class="fas fa-globe"></i> ' + langNames[lang] + ' ▾';
    }
    if (lang === 'ru') {
        const url = window.location.href.replace(/^https?:\/\/translate\.google\.com\/translate\?.*u=/, '');
        window.location.href = decodeURIComponent(url);
        return;
    }
    const currentUrl = window.location.href;
    const translateUrl = `https://translate.google.com/translate?sl=auto&tl=${lang}&u=${encodeURIComponent(currentUrl)}`;
    window.location.href = translateUrl;
}

function toggleWishlist(bookId, btn) {
    fetch('/Account/ToggleWishlist', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ bookId: bookId })
    })
        .then(r => r.json())
        .then(data => {
            if (data.success) {
                if (data.added) {
                    btn.classList.add('active');
                    btn.querySelector('i').style.color = '#e53935';
                } else {
                    btn.classList.remove('active');
                    btn.querySelector('i').style.color = '';
                }
            } else {
                alert(data.message);
            }
        });
}

// QR MODAL - dùng cho trang Payment profile (qrModal)
const qrData = {
    vnpay: {
        title: 'VNPAY',
        desc: 'Платёжный портал VNPAY',
        img: 'https://api.qrserver.com/v1/create-qr-code/?size=200x200&data=VNPAY-PAYMENT-123456'
    },
    momo: {
        title: 'MoMo',
        desc: 'Кошелёк MoMo',
        img: 'https://api.qrserver.com/v1/create-qr-code/?size=200x200&data=MOMO-PAYMENT-123456'
    },
    zalopay: {
        title: 'ZaloPay',
        desc: 'Кошелёк ZaloPay',
        img: 'https://api.qrserver.com/v1/create-qr-code/?size=200x200&data=ZALOPAY-PAYMENT-123456'
    }
};

function showQR(type) {
    const modal = document.getElementById('qrModal');
    if (!modal) return;
    const data = qrData[type];
    document.getElementById('qrTitle').textContent = data.title;
    document.getElementById('qrDesc').textContent = data.desc;
    document.getElementById('qrImg').src = data.img;
    modal.classList.add('open');
}

// closeQR - xử lý cả 2 trường hợp: trang Profile (qrModal) và trang Checkout (qr-modal)
function closeQR() {
    const profileModal = document.getElementById('qrModal');
    if (profileModal) profileModal.classList.remove('open');

    const checkoutModal = document.getElementById('qr-modal');
    if (checkoutModal) checkoutModal.style.display = 'none';

    const overlay = document.getElementById('qr-overlay');
    if (overlay) overlay.style.display = 'none';
}

// CARD MODAL
function openCardModal() {
    const modal = document.getElementById('cardModal');
    if (modal) modal.classList.add('open');
}

function closeCardModal() {
    const modal = document.getElementById('cardModal');
    if (modal) modal.classList.remove('open');
}

function formatCard(input) {
    let val = input.value.replace(/\D/g, '').substring(0, 16);
    input.value = val.replace(/(.{4})/g, '$1 ').trim();
}

function formatExpiry(input) {
    let val = input.value.replace(/\D/g, '').substring(0, 4);
    if (val.length >= 2) val = val.substring(0, 2) + '/' + val.substring(2);
    input.value = val;
}