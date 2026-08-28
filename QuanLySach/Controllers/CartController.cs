// Controllers/CartController.cs — thay thế toàn bộ file
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySach.Models;

namespace QuanLySach.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _db;

        public CartController(AppDbContext db)
        {
            _db = db;
        }

        // Xem giỏ hàng
        public IActionResult Index()
        {
            var sessionId = HttpContext.Session.Id;
            var items = _db.CartItems
                .Where(c => c.SessionId == sessionId)
                .Include(c => c.Book)
                .ToList();

            SetPromoViewData(items);
            return View(items);
        }

        // ===== MÃ GIẢM GIÁ (KHUYẾN MÃI) =====

        // Tìm mã khuyến mãi hợp lệ (đang bật, còn hạn) theo code
        private Promotion? FindValidPromo(string code)
        {
            if (string.IsNullOrWhiteSpace(code)) return null;
            var now = DateTime.Now;
            var trimmed = code.Trim();

            return _db.Promotions.FirstOrDefault(p =>
                p.Code.ToLower() == trimmed.ToLower() &&
                p.IsActive &&
                p.StartDate <= now &&
                p.EndDate >= now);
        }

        // Đưa thông tin giảm giá (nếu có mã đang áp dụng trong session) ra ViewBag để view sử dụng
        private void SetPromoViewData(List<CartItem> items)
        {
            var subTotal = items.Sum(i => i.Book != null ? i.Book.FinalPrice * i.Quantity : 0);
            var promoCode = HttpContext.Session.GetString("PromoCode");
            decimal discountAmount = 0;
            int discountPercent = 0;

            if (!string.IsNullOrEmpty(promoCode))
            {
                var promo = FindValidPromo(promoCode);
                if (promo != null)
                {
                    discountPercent = promo.DiscountPercent;
                    discountAmount = Math.Round(subTotal * discountPercent / 100m, 2);
                }
                else
                {
                    // Mã không còn hợp lệ (hết hạn / bị tắt) -> tự động gỡ khỏi session
                    HttpContext.Session.Remove("PromoCode");
                    promoCode = null;
                }
            }

            ViewBag.PromoCode = promoCode;
            ViewBag.DiscountPercent = discountPercent;
            ViewBag.DiscountAmount = discountAmount;
            ViewBag.SubTotal = subTotal;
            ViewBag.CartTotal = subTotal - discountAmount;
        }

        // Áp dụng mã giảm giá (gọi bằng AJAX từ trang Giỏ hàng hoặc trang Khuyến mãi)
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult ApplyPromo(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return Json(new { success = false, message = "Vui lòng nhập mã giảm giá." });

            var promo = FindValidPromo(code);
            if (promo == null)
                return Json(new { success = false, message = "Mã giảm giá không tồn tại hoặc đã hết hạn." });

            HttpContext.Session.SetString("PromoCode", promo.Code);

            var sessionId = HttpContext.Session.Id;
            var items = _db.CartItems
                .Where(c => c.SessionId == sessionId)
                .Include(c => c.Book)
                .ToList();

            var subTotal = items.Sum(i => i.Book != null ? i.Book.FinalPrice * i.Quantity : 0);
            var discountAmount = Math.Round(subTotal * promo.DiscountPercent / 100m, 2);
            var total = subTotal - discountAmount;

            return Json(new
            {
                success = true,
                message = $"Đã áp dụng mã \"{promo.Code}\" – giảm {promo.DiscountPercent}%.",
                code = promo.Code,
                discountPercent = promo.DiscountPercent,
                subTotal = subTotal.ToString("0.00"),
                discountAmount = discountAmount.ToString("0.00"),
                total = total.ToString("0.00")
            });
        }

        // Gỡ mã giảm giá đang áp dụng
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult RemovePromo()
        {
            HttpContext.Session.Remove("PromoCode");

            var sessionId = HttpContext.Session.Id;
            var items = _db.CartItems
                .Where(c => c.SessionId == sessionId)
                .Include(c => c.Book)
                .ToList();

            var subTotal = items.Sum(i => i.Book != null ? i.Book.FinalPrice * i.Quantity : 0);

            return Json(new
            {
                success = true,
                subTotal = subTotal.ToString("0.00"),
                total = subTotal.ToString("0.00")
            });
        }

        // Thêm vào giỏ
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddToCart(int bookId)
        {
            var book = await _db.Books.FindAsync(bookId);
            if (book == null)
                return Json(new { success = false, message = "Книга не найдена." });

            var sessionId = HttpContext.Session.Id;
            var existing = _db.CartItems
                .FirstOrDefault(c => c.BookId == bookId && c.SessionId == sessionId);

            int currentQtyInCart = existing?.Quantity ?? 0;
            if (currentQtyInCart + 1 > book.Stock)
            {
                return Json(new { success = false, message = "Извините, этой книги больше нет в наличии в нужном количестве." });
            }

            if (existing != null)
                existing.Quantity++;
            else
                _db.CartItems.Add(new CartItem { BookId = bookId, Quantity = 1, SessionId = sessionId });

            await _db.SaveChangesAsync();

            var cartTotal = _db.CartItems
                .Where(c => c.SessionId == sessionId)
                .Include(c => c.Book)
                .Sum(c => c.Book != null ? c.Book.FinalPrice * c.Quantity : 0);

            return Json(new { success = true, cartTotal = cartTotal.ToString("N0") + " ₽" });
        }

        // Xóa item
        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var item = await _db.CartItems.FindAsync(id);
            if (item != null)
            {
                _db.CartItems.Remove(item);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // Trang checkout
        public IActionResult Checkout()
        {
            var sessionId = HttpContext.Session.Id;
            var items = _db.CartItems
                .Where(c => c.SessionId == sessionId)
                .Include(c => c.Book)
                .ToList();

            if (!items.Any())
                return RedirectToAction("Index");

            SetPromoViewData(items);
            return View(items);
        }

        // Xử lý đặt hàng
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(string name, string email, string phone,
        string city, string street, string house, string apartment, string postalCode,
        string deliveryMethod, string paymentMethod)
        {
            var sessionId = HttpContext.Session.Id;
            var items = _db.CartItems
                .Where(c => c.SessionId == sessionId)
                .Include(c => c.Book)
                .ToList();

            if (!items.Any())
                return RedirectToAction("Index");

            // Kiểm tra các trường bắt buộc, tránh lỗi 500 khi thiếu dữ liệu
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(city) ||
                string.IsNullOrWhiteSpace(street) || string.IsNullOrWhiteSpace(house) ||
                string.IsNullOrWhiteSpace(apartment) || string.IsNullOrWhiteSpace(postalCode))
            {
                TempData["ToastError"] = "Пожалуйста, заполните все обязательные поля!";
                return RedirectToAction("Checkout");
            }

            // Kiểm tra lại tồn kho trước khi chốt đơn (phòng trường hợp hết hàng giữa chừng)
            var outOfStockItems = items.Where(i => i.Book == null || i.Quantity > i.Book.Stock).ToList();
            if (outOfStockItems.Any())
            {
                TempData["ToastError"] = "Некоторых книг в вашей корзине больше нет в достаточном количестве. Пожалуйста, проверьте корзину.";
                return RedirectToAction("Index");
            }

            // Áp dụng mã giảm giá (nếu có và còn hợp lệ) cho toàn bộ đơn hàng
            var subTotal = items.Sum(i => i.Book!.FinalPrice * i.Quantity);
            var promoCode = HttpContext.Session.GetString("PromoCode");
            var promo = string.IsNullOrEmpty(promoCode) ? null : FindValidPromo(promoCode);
            var discountPercent = promo?.DiscountPercent ?? 0;
            var discountAmount = promo != null ? Math.Round(subTotal * discountPercent / 100m, 2) : 0;

            var order = new Order
            {
                UserId = HttpContext.Session.GetInt32("UserId"),
                Name = name,
                Email = email,
                Phone = phone ?? "",
                City = city,
                Street = street ?? "",
                House = house ?? "",
                Apartment = apartment ?? "",
                PostalCode = postalCode ?? "",
                DeliveryMethod = deliveryMethod ?? "Курьером",
                DeliveryPrice = 0,
                PromoCode = promo?.Code,
                DiscountPercent = discountPercent,
                DiscountAmount = discountAmount,
                SubTotal = subTotal,
                TotalPrice = subTotal - discountAmount
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            var orderItems = items.Select(i => new OrderItem
            {
                OrderId = order.Id,
                BookId = i.BookId,
                Quantity = i.Quantity,
                Price = i.Book!.FinalPrice
            }).ToList();

            _db.OrderItems.AddRange(orderItems);

            // Trừ tồn kho tương ứng với số lượng đã mua
            foreach (var item in items)
            {
                item.Book!.Stock -= item.Quantity;
                if (item.Book.Stock < 0) item.Book.Stock = 0;
            }

            _db.CartItems.RemoveRange(items);
            await _db.SaveChangesAsync();

            // Đơn hàng đã được tạo, gỡ mã giảm giá khỏi session để không dùng lại cho đơn tiếp theo
            HttpContext.Session.Remove("PromoCode");

            TempData["ToastSuccess"] = "Заказ успешно оформлен! Спасибо за покупку 🎉";
            return RedirectToAction("OrderSuccess", new { id = order.Id });
        }

        // Trang thành công
        public IActionResult OrderSuccess(int id)
        {
            var order = _db.Orders.Find(id);
            if (order == null) return RedirectToAction("Index", "Home");
            return View(order);
        }

        // Lịch sử đơn hàng
        public IActionResult MyOrders()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var orders = _db.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToList();

            return View(orders);
        }

        // Chi tiết đơn hàng
        public IActionResult OrderDetail(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var order = _db.Orders
                .Include(o => o.Items)
                    .ThenInclude(i => i.Book)
                .FirstOrDefault(o => o.Id == id && o.UserId == userId);

            if (order == null) return RedirectToAction("MyOrders");

            return View(order);
        }
    }
}