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
            return View(items);
        }

        // Thêm vào giỏ
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddToCart(int bookId)
        {
            var sessionId = HttpContext.Session.Id;
            var existing = _db.CartItems
                .FirstOrDefault(c => c.BookId == bookId && c.SessionId == sessionId);

            if (existing != null)
                existing.Quantity++;
            else
                _db.CartItems.Add(new CartItem { BookId = bookId, Quantity = 1, SessionId = sessionId });

            await _db.SaveChangesAsync();

            var cartTotal = _db.CartItems
                .Where(c => c.SessionId == sessionId)
                .Include(c => c.Book)
                .Sum(c => c.Book != null ? c.Book.Price * c.Quantity : 0);

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

            var order = new Order
            {
                UserId = HttpContext.Session.GetInt32("UserId"),
                Name = name,
                Email = email,
                Phone = phone,
                City = city,
                Street = street,
                House = house,
                Apartment = apartment,
                PostalCode = postalCode,
                DeliveryMethod = deliveryMethod,
                DeliveryPrice = 0,
                TotalPrice = items.Sum(i => i.Book!.Price * i.Quantity)
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            var orderItems = items.Select(i => new OrderItem
            {
                OrderId = order.Id,
                BookId = i.BookId,
                Quantity = i.Quantity,
                Price = i.Book!.Price
            }).ToList();

            _db.OrderItems.AddRange(orderItems);
            _db.CartItems.RemoveRange(items);
            await _db.SaveChangesAsync();

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