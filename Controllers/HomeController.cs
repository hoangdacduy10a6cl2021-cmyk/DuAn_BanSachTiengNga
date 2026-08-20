using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySach.Models;
using QuanLySach.ViewModels;

namespace QuanLySach.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> Contact()
        {
            var vm = new ContactViewModel();

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                var user = await _db.Users.FindAsync(userId.Value);
                if (user != null)
                {
                    vm.Name = user.Name;
                    vm.Email = user.Email;
                }

                vm.MyMessages = await _db.ContactMessages
                    .Where(m => m.UserId == userId)
                    .OrderByDescending(m => m.CreatedAt)
                    .ToListAsync();

                // Đánh dấu các phản hồi mới là đã xem khi user mở lại trang Contact
                var unreadReplies = vm.MyMessages.Where(m => m.AdminReply != null && !m.IsReplyRead).ToList();
                if (unreadReplies.Any())
                {
                    foreach (var m in unreadReplies) m.IsReplyRead = true;
                    await _db.SaveChangesAsync();
                }
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (!ModelState.IsValid)
            {
                if (userId != null)
                {
                    model.MyMessages = await _db.ContactMessages
                        .Where(m => m.UserId == userId)
                        .OrderByDescending(m => m.CreatedAt)
                        .ToListAsync();
                }
                return View(model);
            }

            var contactMessage = new ContactMessage
            {
                UserId = userId,
                Name = model.Name,
                Email = model.Email,
                Message = model.Message,
                CreatedAt = DateTime.Now
            };

            _db.ContactMessages.Add(contactMessage);
            await _db.SaveChangesAsync();

            TempData["ContactSuccess"] = "Cảm ơn bạn đã liên hệ! Chúng tôi sẽ phản hồi sớm nhất có thể.";
            return RedirectToAction("Contact");
        }
        public IActionResult Promotions()
        {
            var promoCode = HttpContext.Session.GetString("PromoCode");
            if (!string.IsNullOrEmpty(promoCode))
            {
                var now = DateTime.Now;
                var promo = _db.Promotions.FirstOrDefault(p =>
                    p.Code.ToLower() == promoCode.ToLower() &&
                    p.IsActive && p.StartDate <= now && p.EndDate >= now);

                if (promo != null)
                {
                    ViewBag.PromoCode = promo.Code;
                    ViewBag.DiscountPercent = promo.DiscountPercent;
                }
                else
                {
                    HttpContext.Session.Remove("PromoCode");
                }
            }
            return View();
        }
        public IActionResult Popular()
        {
            var popularBooks = _db.Books
                .Where(b => b.IsPopular == true)
                .ToList();
            return View(popularBooks);
        }
        public async Task<IActionResult> NewBooks(int page = 1, string sortOrder = "newest")
        {
            int pageSize = 12;

            var query = _db.Books
                .Where(b => b.IsNew)
                .Include(b => b.Category)
                .AsQueryable();

            query = sortOrder switch
            {
                "priceAsc" => query.OrderBy(b => b.Price),
                "priceDesc" => query.OrderByDescending(b => b.Price),
                "nameAsc" => query.OrderBy(b => b.Title),
                _ => query.OrderByDescending(b => b.CreatedDate)
            };

            var totalItems = await query.CountAsync();
            var books = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.SortOrder = sortOrder;

            return View(books);
        }
        public IActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            // Chưa đăng nhập → chuyển sang trang Đăng ký
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Register", "Account");
            }

            var vm = new HomeViewModel
            {
                PopularBooks = await _db.Books
                    .Where(b => b.IsPopular)
                    .Include(b => b.Category)
                    .Take(6)
                    .ToListAsync(),

                NewBooks = await _db.Books
                    .Where(b => b.IsNew)
                    .Take(4)
                    .ToListAsync(),

                Categories = await _db.Categories.ToListAsync()
            };

            return View(vm);
        }
    }
}