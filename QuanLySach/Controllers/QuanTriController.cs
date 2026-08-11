using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySach.Models;
using System.Security.Cryptography;
using System.Text;

namespace QuanLySach.Controllers
{
    public class QuanTriController : Controller
    {
        private readonly AppDbContext _db;

        public QuanTriController(AppDbContext db)
        {
            _db = db;
        }

        // ===== HELPER: kiểm tra đã đăng nhập admin chưa =====
        private bool IsAdminLoggedIn()
        {
            return HttpContext.Session.GetInt32("AdminId") != null;
        }

        private IActionResult? RequireAdmin()
        {
            if (!IsAdminLoggedIn())
                return RedirectToAction("Login", "Account");
            return null;
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        // Đã bọc try-catch an toàn để không bị văng lỗi 500 nếu bảng Orders chưa sẵn sàng
        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            try
            {
                if (IsAdminLoggedIn())
                {
                    ViewBag.NewOrdersCount = _db.Orders.Count(o => o.Status == "Новый");
                }
            }
            catch
            {
                ViewBag.NewOrdersCount = 0;
            }
        }
        // ================= LOGIN / LOGOUT =================
        [HttpGet]
        public IActionResult Login()
        {
            if (IsAdminLoggedIn())
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            var admin = _db.Admins.Include(a => a.Role).FirstOrDefault(a => a.Username == username);

            if (admin == null || admin.PasswordHash != HashPassword(password))
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu";
                return View();
            }

            HttpContext.Session.SetInt32("AdminId", admin.Id);
            HttpContext.Session.SetString("AdminName", admin.FullName);
            HttpContext.Session.SetString("AdminRole", admin.Role?.Name ?? "");

            return RedirectToAction("Index");
        }

        [HttpPost]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminId");
            HttpContext.Session.Remove("AdminName");
            return RedirectToAction("Login");
        }

        // ================= DASHBOARD =================
        public async Task<IActionResult> Index()
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            ViewBag.TotalBooks = await _db.Books.CountAsync();
            ViewBag.TotalCategories = await _db.Categories.CountAsync();
            ViewBag.TotalOrders = await _db.Orders.CountAsync();
            ViewBag.TotalUsers = await _db.Users.CountAsync();
            ViewBag.TotalRevenue = await _db.Orders.SumAsync(o => (decimal?)o.TotalPrice) ?? 0;

            ViewBag.RecentOrders = await _db.Orders
                .OrderByDescending(o => o.CreatedAt)
                .Take(5)
                .ToListAsync();

            ViewBag.RecentBooks = await _db.Books
                .Include(b => b.Category)
                .OrderByDescending(b => b.Id)
                .Take(5)
                .ToListAsync();

            var topSelling = await _db.OrderItems
                .GroupBy(oi => oi.BookId)
                .Select(g => new { BookId = g.Key, SoldQty = g.Sum(x => x.Quantity) })
                .OrderByDescending(x => x.SoldQty)
                .Take(5)
                .ToListAsync();

            var topBookIds = topSelling.Select(x => x.BookId).ToList();
            var topBooksData = await _db.Books.Where(b => topBookIds.Contains(b.Id)).ToListAsync();

            ViewBag.TopSellingBooks = topSelling
                .Select(x => new
                {
                    Book = topBooksData.FirstOrDefault(b => b.Id == x.BookId),
                    x.SoldQty
                })
                .Where(x => x.Book != null)
                .ToList();

            var currentYear = DateTime.Now.Year;
            var ordersThisYear = await _db.Orders
                .Where(o => o.CreatedAt.Year == currentYear)
                .ToListAsync();

            var monthlyRevenue = new decimal[12];
            foreach (var order in ordersThisYear)
            {
                monthlyRevenue[order.CreatedAt.Month - 1] += order.TotalPrice;
            }

            ViewBag.ChartLabels = string.Join(",", Enumerable.Range(1, 12).Select(m => $"'Th{m}'"));
            ViewBag.ChartValues = string.Join(",", monthlyRevenue.Select(v => v.ToString(System.Globalization.CultureInfo.InvariantCulture)));

            return View();
        }

        // ================= BOOKS =================
        public async Task<IActionResult> Books(string? searchTerm)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            var query = _db.Books.Include(b => b.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm));
            }

            var books = await query.OrderByDescending(b => b.Id).ToListAsync();
            ViewBag.SearchTerm = searchTerm;
            return View(books);
        }

        [HttpGet]
        public async Task<IActionResult> BookCreate()
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            ViewBag.Categories = await _db.Categories.ToListAsync();
            return View(new Book());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookCreate(Book model)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _db.Categories.ToListAsync();
                return View(model);
            }

            _db.Books.Add(model);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Đã thêm sách mới thành công!";
            return RedirectToAction("Books");
        }

        [HttpGet]
        public async Task<IActionResult> BookEdit(int id)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            var book = await _db.Books.FindAsync(id);
            if (book == null) return NotFound();

            ViewBag.Categories = await _db.Categories.ToListAsync();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookEdit(int id, Book model)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _db.Categories.ToListAsync();
                return View(model);
            }

            _db.Books.Update(model);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Đã cập nhật sách thành công!";
            return RedirectToAction("Books");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookDelete(int id)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            var book = await _db.Books.FindAsync(id);
            if (book != null)
            {
                _db.Books.Remove(book);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Đã xóa sách!";
            }
            return RedirectToAction("Books");
        }

        // ================= CATEGORIES =================
        public async Task<IActionResult> Categories()
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            var categories = await _db.Categories.Include(c => c.Books).OrderBy(c => c.Name).ToListAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult CategoryCreate()
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;
            return View(new Category());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryCreate(Category model)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            if (!ModelState.IsValid) return View(model);

            _db.Categories.Add(model);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Đã thêm danh mục mới!";
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public async Task<IActionResult> CategoryEdit(int id)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            var category = await _db.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryEdit(int id, Category model)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);

            _db.Categories.Update(model);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Đã cập nhật danh mục!";
            return RedirectToAction("Categories");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryDelete(int id)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            var category = await _db.Categories.Include(c => c.Books).FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                if (category.Books.Any())
                {
                    TempData["Error"] = "Không thể xóa danh mục vì vẫn còn sách thuộc danh mục này!";
                    return RedirectToAction("Categories");
                }
                _db.Categories.Remove(category);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Đã xóa danh mục!";
            }
            return RedirectToAction("Categories");
        }

        // ================= ORDERS =================
        public async Task<IActionResult> Orders()
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            var orders = await _db.Orders.OrderByDescending(o => o.CreatedAt).ToListAsync();
            return View(orders);
        }

        public async Task<IActionResult> OrderDetails(int id)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            var order = await _db.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Book)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return NotFound();
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderUpdateStatus(int id, string status)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            var order = await _db.Orders.FindAsync(id);
            if (order != null)
            {
                order.Status = status;
                await _db.SaveChangesAsync();
                TempData["Success"] = "Đã cập nhật trạng thái đơn hàng!";
            }
            return RedirectToAction("OrderDetails", new { id });
        }

        // ================= USERS =================
        public async Task<IActionResult> Users()
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            var users = await _db.Users.OrderByDescending(u => u.CreatedAt).ToListAsync();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserDelete(int id)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            var user = await _db.Users.FindAsync(id);
            if (user != null)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Đã xóa tài khoản người dùng!";
            }
            return RedirectToAction("Users");
        }

        // ================= ROLES =================
        public async Task<IActionResult> Roles()
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            var roles = await _db.Roles.Include(r => r.Admins).OrderBy(r => r.Id).ToListAsync();
            return View(roles);
        }

        [HttpGet]
        public IActionResult RoleCreate()
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;
            return View(new Role());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleCreate(Role model)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            if (!ModelState.IsValid) return View(model);

            _db.Roles.Add(model);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Đã thêm vai trò mới!";
            return RedirectToAction("Roles");
        }

        [HttpGet]
        public async Task<IActionResult> RoleEdit(int id)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            var role = await _db.Roles.FindAsync(id);
            if (role == null) return NotFound();
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleEdit(int id, Role model)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);

            _db.Roles.Update(model);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Đã cập nhật vai trò!";
            return RedirectToAction("Roles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleDelete(int id)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            var role = await _db.Roles.Include(r => r.Admins).FirstOrDefaultAsync(r => r.Id == id);
            if (role != null)
            {
                if (role.Admins.Any())
                {
                    TempData["Error"] = "Không thể xóa vai trò vì vẫn còn tài khoản admin thuộc vai trò này!";
                    return RedirectToAction("Roles");
                }
                _db.Roles.Remove(role);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Đã xóa vai trò!";
            }
            return RedirectToAction("Roles");
        }

        // ================= ADMIN ACCOUNTS =================
        public async Task<IActionResult> AdminAccounts()
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            var accounts = await _db.Admins.Include(a => a.Role).OrderBy(a => a.Id).ToListAsync();
            return View(accounts);
        }

        [HttpGet]
        public async Task<IActionResult> AdminAccountCreate()
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            ViewBag.Roles = await _db.Roles.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminAccountCreate(string username, string password, string fullName, int roleId)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            if (await _db.Admins.AnyAsync(a => a.Username == username))
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại!";
                ViewBag.Roles = await _db.Roles.ToListAsync();
                return View();
            }

            var admin = new Admin
            {
                Username = username,
                PasswordHash = HashPassword(password),
                FullName = fullName,
                RoleId = roleId
            };
            _db.Admins.Add(admin);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Đã thêm tài khoản admin mới!";
            return RedirectToAction("AdminAccounts");
        }

        [HttpGet]
        public async Task<IActionResult> AdminAccountEdit(int id)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            var admin = await _db.Admins.FindAsync(id);
            if (admin == null) return NotFound();

            ViewBag.Roles = await _db.Roles.ToListAsync();
            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminAccountEdit(int id, string fullName, int roleId, string? newPassword)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            var admin = await _db.Admins.FindAsync(id);
            if (admin == null) return NotFound();

            admin.FullName = fullName;
            admin.RoleId = roleId;
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                admin.PasswordHash = HashPassword(newPassword);
            }

            await _db.SaveChangesAsync();
            TempData["Success"] = "Đã cập nhật tài khoản!";
            return RedirectToAction("AdminAccounts");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminAccountDelete(int id)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            var currentAdminId = HttpContext.Session.GetInt32("AdminId");
            if (id == currentAdminId)
            {
                TempData["Error"] = "Không thể xóa tài khoản đang đăng nhập!";
                return RedirectToAction("AdminAccounts");
            }

            if (await _db.Admins.CountAsync() <= 1)
            {
                TempData["Error"] = "Phải có ít nhất 1 tài khoản admin!";
                return RedirectToAction("AdminAccounts");
            }

            var admin = await _db.Admins.FindAsync(id);
            if (admin != null)
            {
                _db.Admins.Remove(admin);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Đã xóa tài khoản admin!";
            }
            return RedirectToAction("AdminAccounts");
        }
    }
}