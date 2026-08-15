using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySach.Models;
using QuanLySach.ViewModels;
using System.Security.Cryptography;
using System.Text;

namespace QuanLySach.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;

        public AccountController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_db.Users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Этот e-mail уже зарегистрирован");
                return View(model);
            }

            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                PasswordHash = HashPassword(model.Password)
            };

            _db.Users.Add(user);
            _db.SaveChanges();

            TempData["RegisterSuccess"] = "Регистрация прошла успешно! Войдите в аккаунт.";
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("AdminId") != null)
                return RedirectToAction("Index", "QuanTri");
            if (HttpContext.Session.GetInt32("UserId") != null)
                return RedirectToAction("Index", "Home");

            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Kiểm tra xem có phải tài khoản Admin không — nếu đúng, chuyển thẳng vào trang quản trị
            var admin = await _db.Admins.Include(a => a.Role).FirstOrDefaultAsync(a => a.Username == model.Email);
            if (admin != null && admin.PasswordHash == HashPassword(model.Password))
            {
                HttpContext.Session.SetInt32("AdminId", admin.Id);
                HttpContext.Session.SetString("AdminName", admin.FullName);
                HttpContext.Session.SetString("AdminRole", admin.Role?.Name ?? "");
                HttpContext.Session.SetInt32("AdminRoleId", admin.RoleId);
                TempData["ToastSuccess"] = "Добро пожаловать, администратор!";
                return RedirectToAction("Index", "QuanTri");
            }

            var user = _db.Users.FirstOrDefault(u => u.Email == model.Email);

            if (user == null || user.PasswordHash != HashPassword(model.Password))
            {
                ModelState.AddModelError("", "Неверный e-mail или пароль");
                return View(model);
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("UserEmail", user.Email);
            TempData["ToastSuccess"] = "Вы успешно вошли в систему! 👋";

            if (model.RememberMe)
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(30),
                    HttpOnly = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.Lax
                };
                Response.Cookies.Append("RememberUserId", user.Id.ToString(), cookieOptions);
                Response.Cookies.Append("RememberUserName", user.Name, cookieOptions);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("RememberUserId");
            Response.Cookies.Delete("RememberUserName");
            TempData["ToastSuccess"] = "Вы успешно вышли из системы!";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ViewBag.Error = "Введите ваш e-mail";
                return View();
            }

            var user = _db.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                user.PasswordHash = HashPassword("123456");
                _db.SaveChanges();
            }

            TempData["ForgotSuccess"] = "Если этот e-mail зарегистрирован, временный пароль: 123456";
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var user = _db.Users.Find(userId);
            if (user == null) return RedirectToAction("Login");

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(string name, string email, string phone)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var user = _db.Users.Find(userId);
            if (user != null)
            {
                user.Name = name;
                user.Email = email;
                user.Phone = phone;
                _db.SaveChanges();
                HttpContext.Session.SetString("UserName", user.Name);
                TempData["Success"] = "Изменения сохранены!";
            }

            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult Orders()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var orders = _db.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToList();

            return View(orders);
        }

        [HttpGet]
        public IActionResult Wishlist()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var items = _db.Wishlists
                .Where(w => w.UserId == userId)
                .Include(w => w.Book)
                .ToList();

            return View(items);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> ToggleWishlist([FromBody] WishlistRequest request)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return Json(new { success = false, message = "Войдите в аккаунт" });

            var existing = _db.Wishlists
                .FirstOrDefault(w => w.UserId == userId && w.BookId == request.BookId);

            if (existing != null)
            {
                _db.Wishlists.Remove(existing);
                await _db.SaveChangesAsync();
                return Json(new { success = true, added = false });
            }
            else
            {
                _db.Wishlists.Add(new Wishlist { UserId = userId.Value, BookId = request.BookId });
                await _db.SaveChangesAsync();
                return Json(new { success = true, added = true });
            }
        }

        [HttpGet]
        public IActionResult Addresses()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");
            return View();
        }

        [HttpGet]
        public IActionResult Payment()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var cards = _db.PaymentCards
                .Where(c => c.UserId == userId)
                .ToList();

            return View(cards);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCard(string cardNumber, string cardName, string expiry)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var digits = cardNumber.Replace(" ", "");
            var last4 = digits.Length >= 4
                ? digits.Substring(digits.Length - 4)
                : digits;

            var card = new PaymentCard
            {
                UserId = userId.Value,
                CardNumber = "**** **** **** " + last4,
                CardName = cardName.ToUpper(),
                Expiry = expiry,
                CardType = "VISA"
            };

            _db.PaymentCards.Add(card);
            await _db.SaveChangesAsync();

            TempData["CardSuccess"] = "Карта успешно добавлена!";
            return RedirectToAction("Payment");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveCard(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var card = await _db.PaymentCards
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (card != null)
            {
                _db.PaymentCards.Remove(card);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("Payment");
        }

        [HttpGet]
        public IActionResult Settings()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Settings(string currentPassword, string newPassword)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var user = _db.Users.Find(userId);
            if (user == null) return RedirectToAction("Login");

            if (user.PasswordHash != HashPassword(currentPassword))
            {
                TempData["Error"] = "Неверный текущий пароль!";
                return RedirectToAction("Settings");
            }

            user.PasswordHash = HashPassword(newPassword);
            _db.SaveChanges();
            TempData["Success"] = "Пароль успешно изменён!";
            return RedirectToAction("Settings");
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> GetSettings()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return Json(new { success = false });

            var user = await _db.Users.FindAsync(userId);
            if (user == null)
                return Json(new { success = false });

            return Json(new
            {
                success = true,
                showCyrillic = user.ShowCyrillic,
                showTransliteration = user.ShowTransliteration,
                emailNotifications = user.EmailNotifications,
                smsNotifications = user.SmsNotifications,
                language = user.Language,
                currency1 = user.Currency1,
                currency2 = user.Currency2,
                currency3 = user.Currency3
            });
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> SaveSettings([FromBody] SettingsRequest request)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return Json(new { success = false, message = "Войдите в аккаунт" });

            var user = await _db.Users.FindAsync(userId);
            if (user == null)
                return Json(new { success = false, message = "Пользователь не найден" });

            user.ShowCyrillic = request.ShowCyrillic;
            user.ShowTransliteration = request.ShowTransliteration;
            user.EmailNotifications = request.EmailNotifications;
            user.SmsNotifications = request.SmsNotifications;
            user.Language = request.Language;
            user.Currency1 = request.Currency1;
            user.Currency2 = request.Currency2;
            user.Currency3 = request.Currency3;

            await _db.SaveChangesAsync();
            return Json(new { success = true });
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }

    public class WishlistRequest
    {
        public int BookId { get; set; }
    }

    public class SettingsRequest
    {
        public bool ShowCyrillic { get; set; }
        public bool ShowTransliteration { get; set; }
        public bool EmailNotifications { get; set; }
        public bool SmsNotifications { get; set; }
        public string Language { get; set; } = "Вьетнам";
        public string Currency1 { get; set; } = "VND";
        public string Currency2 { get; set; } = "RUB";
        public string Currency3 { get; set; } = "USD";
    }
}