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

        // ===== HELPER: kiểm tra quyền theo module (Books, Categories, Orders, ...) và hành động (View/Create/Edit/Delete) =====
        private bool HasPermission(string module, string action)
        {
            var roleId = HttpContext.Session.GetInt32("AdminRoleId");
            if (roleId == null) return false;

            var role = _db.Roles.Include(r => r.Permissions).FirstOrDefault(r => r.Id == roleId);
            if (role == null) return false;

            // Vai trò siêu quản trị luôn có toàn quyền, không bị giới hạn bởi ma trận phân quyền
            if (role.IsSuperAdmin) return true;

            var perm = role.Permissions.FirstOrDefault(p => p.Module == module);
            if (perm == null) return false;

            return action switch
            {
                "View" => perm.CanView,
                "Create" => perm.CanCreate,
                "Edit" => perm.CanEdit,
                "Delete" => perm.CanDelete,
                _ => false
            };
        }

        private IActionResult? RequirePermission(string module, string action)
        {
            if (RequireAdmin() is IActionResult redirect) return redirect;

            if (!HasPermission(module, action))
            {
                TempData["Error"] = "Bạn không có quyền thực hiện thao tác này! Vui lòng liên hệ quản trị viên.";
                return RedirectToAction("Index");
            }
            return null;
        }

        // ===== HELPER: ghi nhật ký hoạt động =====
        private void LogActivity(string module, string action, string description)
        {
            try
            {
                var log = new ActivityLog
                {
                    AdminId = HttpContext.Session.GetInt32("AdminId"),
                    AdminName = HttpContext.Session.GetString("AdminName") ?? "Hệ thống",
                    Module = module,
                    Action = action,
                    Description = description,
                    CreatedAt = DateTime.Now,
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? ""
                };
                _db.ActivityLogs.Add(log);
                _db.SaveChanges();
            }
            catch
            {
                // Không để lỗi ghi log làm gián đoạn thao tác chính
            }
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
                    ViewBag.NewMessagesCount = _db.ContactMessages.Count(m => !m.IsRead);
                }
            }
            catch
            {
                ViewBag.NewOrdersCount = 0;
                ViewBag.NewMessagesCount = 0;
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
            HttpContext.Session.SetInt32("AdminRoleId", admin.RoleId);

            LogActivity("Auth", "Đăng nhập", $"{admin.FullName} đã đăng nhập vào hệ thống quản trị.");

            return RedirectToAction("Index");
        }

        [HttpPost]
        [HttpGet]
        public IActionResult Logout()
        {
            if (IsAdminLoggedIn())
            {
                var adminNameAtLogout = HttpContext.Session.GetString("AdminName");
                LogActivity("Auth", "Đăng xuất", $"{adminNameAtLogout} đã đăng xuất khỏi hệ thống quản trị.");
            }
            HttpContext.Session.Remove("AdminId");
            HttpContext.Session.Remove("AdminName");
            HttpContext.Session.Remove("AdminRole");
            HttpContext.Session.Remove("AdminRoleId");
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
            if (RequirePermission("Books", "View") is IActionResult redirect) return redirect;

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
            if (RequirePermission("Books", "Create") is IActionResult redirect) return redirect;

            ViewBag.Categories = await _db.Categories.ToListAsync();
            return View(new Book());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookCreate(Book model)
        {
            if (RequirePermission("Books", "Create") is IActionResult redirect) return redirect;

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _db.Categories.ToListAsync();
                return View(model);
            }

            _db.Books.Add(model);
            await _db.SaveChangesAsync();
            LogActivity("Books", "Thêm sách", "Đã thêm sách mới thành công!");
            TempData["Success"] = "Đã thêm sách mới thành công!";
            return RedirectToAction("Books");
        }

        [HttpGet]
        public async Task<IActionResult> BookEdit(int id)
        {
            if (RequirePermission("Books", "Edit") is IActionResult redirect) return redirect;

            var book = await _db.Books.FindAsync(id);
            if (book == null) return NotFound();

            ViewBag.Categories = await _db.Categories.ToListAsync();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookEdit(int id, Book model)
        {
            if (RequirePermission("Books", "Edit") is IActionResult redirect) return redirect;

            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _db.Categories.ToListAsync();
                return View(model);
            }

            _db.Books.Update(model);
            await _db.SaveChangesAsync();
            LogActivity("Books", "Sửa sách", "Đã cập nhật sách thành công!");
            TempData["Success"] = "Đã cập nhật sách thành công!";
            return RedirectToAction("Books");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookDelete(int id)
        {
            if (RequirePermission("Books", "Delete") is IActionResult redirect) return redirect;

            var book = await _db.Books.FindAsync(id);
            if (book != null)
            {
                _db.Books.Remove(book);
                await _db.SaveChangesAsync();
                LogActivity("Books", "Xóa sách", "Đã xóa sách!");
                TempData["Success"] = "Đã xóa sách!";
            }
            return RedirectToAction("Books");
        }

        // ================= CATEGORIES =================
        public async Task<IActionResult> Categories()
        {
            if (RequirePermission("Categories", "View") is IActionResult redirect) return redirect;

            var categories = await _db.Categories.Include(c => c.Books).OrderBy(c => c.Name).ToListAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult CategoryCreate()
        {
            if (RequirePermission("Categories", "Create") is IActionResult redirect) return redirect;
            return View(new Category());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryCreate(Category model)
        {
            if (RequirePermission("Categories", "Create") is IActionResult redirect) return redirect;

            if (!ModelState.IsValid) return View(model);

            _db.Categories.Add(model);
            await _db.SaveChangesAsync();
            LogActivity("Categories", "Thêm danh mục", "Đã thêm danh mục mới!");
            TempData["Success"] = "Đã thêm danh mục mới!";
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public async Task<IActionResult> CategoryEdit(int id)
        {
            if (RequirePermission("Categories", "Edit") is IActionResult redirect) return redirect;

            var category = await _db.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryEdit(int id, Category model)
        {
            if (RequirePermission("Categories", "Edit") is IActionResult redirect) return redirect;

            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);

            _db.Categories.Update(model);
            await _db.SaveChangesAsync();
            LogActivity("Categories", "Sửa danh mục", "Đã cập nhật danh mục!");
            TempData["Success"] = "Đã cập nhật danh mục!";
            return RedirectToAction("Categories");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryDelete(int id)
        {
            if (RequirePermission("Categories", "Delete") is IActionResult redirect) return redirect;

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
                LogActivity("Categories", "Xóa danh mục", "Đã xóa danh mục!");
                TempData["Success"] = "Đã xóa danh mục!";
            }
            return RedirectToAction("Categories");
        }
        // ================= AUTHORS =================
        public async Task<IActionResult> Authors(string? searchTerm)
        {
            if (RequirePermission("Authors", "View") is IActionResult redirect) return redirect;

            var query = _db.Authors.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(a => a.Name.Contains(searchTerm));
            }

            var authors = await query.OrderBy(a => a.Name).ToListAsync();
            ViewBag.SearchTerm = searchTerm;
            return View(authors);
        }

        [HttpGet]
        public IActionResult AuthorCreate()
        {
            if (RequirePermission("Authors", "Create") is IActionResult redirect) return redirect;
            return View(new Author());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AuthorCreate(Author model)
        {
            if (RequirePermission("Authors", "Create") is IActionResult redirect) return redirect;

            if (!ModelState.IsValid) return View(model);

            _db.Authors.Add(model);
            await _db.SaveChangesAsync();
            LogActivity("Authors", "Thêm tác giả", "Đã thêm tác giả mới!");
            TempData["Success"] = "Đã thêm tác giả mới!";
            return RedirectToAction("Authors");
        }

        [HttpGet]
        public async Task<IActionResult> AuthorEdit(int id)
        {
            if (RequirePermission("Authors", "Edit") is IActionResult redirect) return redirect;

            var author = await _db.Authors.FindAsync(id);
            if (author == null) return NotFound();
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AuthorEdit(int id, Author model)
        {
            if (RequirePermission("Authors", "Edit") is IActionResult redirect) return redirect;

            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);

            _db.Authors.Update(model);
            await _db.SaveChangesAsync();
            LogActivity("Authors", "Sửa tác giả", "Đã cập nhật tác giả!");
            TempData["Success"] = "Đã cập nhật tác giả!";
            return RedirectToAction("Authors");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AuthorDelete(int id)
        {
            if (RequirePermission("Authors", "Delete") is IActionResult redirect) return redirect;

            var author = await _db.Authors.FindAsync(id);
            if (author != null)
            {
                _db.Authors.Remove(author);
                await _db.SaveChangesAsync();
                LogActivity("Authors", "Xóa tác giả", "Đã xóa tác giả!");
                TempData["Success"] = "Đã xóa tác giả!";
            }
            return RedirectToAction("Authors");
        }

        // ================= PUBLISHERS =================
        public async Task<IActionResult> Publishers(string? searchTerm)
        {
            if (RequirePermission("Publishers", "View") is IActionResult redirect) return redirect;

            var query = _db.Publishers.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm));
            }

            var publishers = await query.OrderBy(p => p.Name).ToListAsync();
            ViewBag.SearchTerm = searchTerm;
            return View(publishers);
        }

        [HttpGet]
        public IActionResult PublisherCreate()
        {
            if (RequirePermission("Publishers", "Create") is IActionResult redirect) return redirect;
            return View(new Publisher());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PublisherCreate(Publisher model)
        {
            if (RequirePermission("Publishers", "Create") is IActionResult redirect) return redirect;

            if (!ModelState.IsValid) return View(model);

            _db.Publishers.Add(model);
            await _db.SaveChangesAsync();
            LogActivity("Publishers", "Thêm nhà xuất bản", "Đã thêm nhà xuất bản mới!");
            TempData["Success"] = "Đã thêm nhà xuất bản mới!";
            return RedirectToAction("Publishers");
        }

        [HttpGet]
        public async Task<IActionResult> PublisherEdit(int id)
        {
            if (RequirePermission("Publishers", "Edit") is IActionResult redirect) return redirect;

            var publisher = await _db.Publishers.FindAsync(id);
            if (publisher == null) return NotFound();
            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PublisherEdit(int id, Publisher model)
        {
            if (RequirePermission("Publishers", "Edit") is IActionResult redirect) return redirect;

            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);

            _db.Publishers.Update(model);
            await _db.SaveChangesAsync();
            LogActivity("Publishers", "Sửa nhà xuất bản", "Đã cập nhật nhà xuất bản!");
            TempData["Success"] = "Đã cập nhật nhà xuất bản!";
            return RedirectToAction("Publishers");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PublisherDelete(int id)
        {
            if (RequirePermission("Publishers", "Delete") is IActionResult redirect) return redirect;

            var publisher = await _db.Publishers.FindAsync(id);
            if (publisher != null)
            {
                _db.Publishers.Remove(publisher);
                await _db.SaveChangesAsync();
                LogActivity("Publishers", "Xóa nhà xuất bản", "Đã xóa nhà xuất bản!");
                TempData["Success"] = "Đã xóa nhà xuất bản!";
            }
            return RedirectToAction("Publishers");
        }

        // ================= PROMOTIONS =================
        public async Task<IActionResult> Promotions()
        {
            if (RequirePermission("Promotions", "View") is IActionResult redirect) return redirect;

            var promotions = await _db.Promotions.OrderByDescending(p => p.StartDate).ToListAsync();
            return View(promotions);
        }

        [HttpGet]
        public IActionResult PromotionCreate()
        {
            if (RequirePermission("Promotions", "Create") is IActionResult redirect) return redirect;
            return View(new Promotion());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PromotionCreate(Promotion model)
        {
            if (RequirePermission("Promotions", "Create") is IActionResult redirect) return redirect;

            if (!ModelState.IsValid) return View(model);
            if (model.EndDate < model.StartDate)
            {
                ModelState.AddModelError("", "Ngày kết thúc phải sau ngày bắt đầu!");
                return View(model);
            }

            _db.Promotions.Add(model);
            await _db.SaveChangesAsync();
            LogActivity("Promotions", "Thêm khuyến mãi", "Đã thêm khuyến mãi mới!");
            TempData["Success"] = "Đã thêm khuyến mãi mới!";
            return RedirectToAction("Promotions");
        }

        [HttpGet]
        public async Task<IActionResult> PromotionEdit(int id)
        {
            if (RequirePermission("Promotions", "Edit") is IActionResult redirect) return redirect;

            var promotion = await _db.Promotions.FindAsync(id);
            if (promotion == null) return NotFound();
            return View(promotion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PromotionEdit(int id, Promotion model)
        {
            if (RequirePermission("Promotions", "Edit") is IActionResult redirect) return redirect;

            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);
            if (model.EndDate < model.StartDate)
            {
                ModelState.AddModelError("", "Ngày kết thúc phải sau ngày bắt đầu!");
                return View(model);
            }

            _db.Promotions.Update(model);
            await _db.SaveChangesAsync();
            LogActivity("Promotions", "Sửa khuyến mãi", "Đã cập nhật khuyến mãi!");
            TempData["Success"] = "Đã cập nhật khuyến mãi!";
            return RedirectToAction("Promotions");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PromotionDelete(int id)
        {
            if (RequirePermission("Promotions", "Delete") is IActionResult redirect) return redirect;

            var promotion = await _db.Promotions.FindAsync(id);
            if (promotion != null)
            {
                _db.Promotions.Remove(promotion);
                await _db.SaveChangesAsync();
                LogActivity("Promotions", "Xóa khuyến mãi", "Đã xóa khuyến mãi!");
                TempData["Success"] = "Đã xóa khuyến mãi!";
            }
            return RedirectToAction("Promotions");
        }

        // ================= ORDERS =================
        public async Task<IActionResult> Orders()
        {
            if (RequirePermission("Orders", "View") is IActionResult redirect) return redirect;

            var orders = await _db.Orders.OrderByDescending(o => o.CreatedAt).ToListAsync();
            return View(orders);
        }

        public async Task<IActionResult> OrderDetails(int id)
        {
            if (RequirePermission("Orders", "View") is IActionResult redirect) return redirect;

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
            if (RequirePermission("Orders", "Edit") is IActionResult redirect) return redirect;

            var order = await _db.Orders.FindAsync(id);
            if (order != null)
            {
                order.Status = status;
                await _db.SaveChangesAsync();
                LogActivity("Orders", "Cập nhật trạng thái đơn hàng", "Đã cập nhật trạng thái đơn hàng!");
                TempData["Success"] = "Đã cập nhật trạng thái đơn hàng!";
            }
            return RedirectToAction("OrderDetails", new { id });
        }

        // ================= USERS =================
        public async Task<IActionResult> Users()
        {
            if (RequirePermission("Users", "View") is IActionResult redirect) return redirect;

            var users = await _db.Users.OrderByDescending(u => u.CreatedAt).ToListAsync();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserDelete(int id)
        {
            if (RequirePermission("Users", "Delete") is IActionResult redirect) return redirect;

            var user = await _db.Users.FindAsync(id);
            if (user != null)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                LogActivity("Users", "Xóa người dùng", "Đã xóa tài khoản người dùng!");
                TempData["Success"] = "Đã xóa tài khoản người dùng!";
            }
            return RedirectToAction("Users");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserResetPassword(int id, string newPassword)
        {
            if (RequirePermission("Users", "Edit") is IActionResult redirect) return redirect;

            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
            {
                TempData["Error"] = "Mật khẩu mới phải có ít nhất 6 ký tự!";
                return RedirectToAction("Users");
            }

            var user = await _db.Users.FindAsync(id);
            if (user != null)
            {
                user.PasswordHash = HashPassword(newPassword);
                await _db.SaveChangesAsync();
                LogActivity("Users", "Đặt lại mật khẩu người dùng", $"Đã cấp lại mật khẩu cho {user.Name}!");
                TempData["Success"] = $"Đã cấp lại mật khẩu cho {user.Name}!";
            }
            return RedirectToAction("Users");
        }

        // ================= ROLES =================
        public async Task<IActionResult> Roles()
        {
            if (RequirePermission("Roles", "View") is IActionResult redirect) return redirect;

            var roles = await _db.Roles.Include(r => r.Admins).OrderBy(r => r.Id).ToListAsync();
            return View(roles);
        }

        [HttpGet]
        public IActionResult RoleCreate()
        {
            if (RequirePermission("Roles", "Create") is IActionResult redirect) return redirect;
            return View(new Role());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleCreate(Role model)
        {
            if (RequirePermission("Roles", "Create") is IActionResult redirect) return redirect;

            if (!ModelState.IsValid) return View(model);

            _db.Roles.Add(model);
            await _db.SaveChangesAsync();
            LogActivity("Roles", "Thêm vai trò", "Đã thêm vai trò mới!");
            TempData["Success"] = "Đã thêm vai trò mới!";
            return RedirectToAction("Roles");
        }

        [HttpGet]
        public async Task<IActionResult> RoleEdit(int id)
        {
            if (RequirePermission("Roles", "Edit") is IActionResult redirect) return redirect;

            var role = await _db.Roles.FindAsync(id);
            if (role == null) return NotFound();
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleEdit(int id, Role model)
        {
            if (RequirePermission("Roles", "Edit") is IActionResult redirect) return redirect;

            if (id != model.Id) return NotFound();
            if (!ModelState.IsValid) return View(model);

            var role = await _db.Roles.FindAsync(id);
            if (role == null) return NotFound();

            // Chỉ cập nhật tên/mô tả — không cho phép form này thay đổi cờ IsSuperAdmin (chỉ set qua seed data hệ thống)
            role.Name = model.Name;
            role.Description = model.Description;

            await _db.SaveChangesAsync();
            LogActivity("Roles", "Sửa vai trò", "Đã cập nhật vai trò!");
            TempData["Success"] = "Đã cập nhật vai trò!";
            return RedirectToAction("Roles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleDelete(int id)
        {
            if (RequirePermission("Roles", "Delete") is IActionResult redirect) return redirect;

            var role = await _db.Roles.Include(r => r.Admins).FirstOrDefaultAsync(r => r.Id == id);
            if (role != null)
            {
                if (role.IsSuperAdmin)
                {
                    TempData["Error"] = "Không thể xóa vai trò siêu quản trị!";
                    return RedirectToAction("Roles");
                }
                if (role.Admins.Any())
                {
                    TempData["Error"] = "Không thể xóa vai trò vì vẫn còn tài khoản admin thuộc vai trò này!";
                    return RedirectToAction("Roles");
                }
                _db.Roles.Remove(role);
                await _db.SaveChangesAsync();
                LogActivity("Roles", "Xóa vai trò", "Đã xóa vai trò!");
                TempData["Success"] = "Đã xóa vai trò!";
            }
            return RedirectToAction("Roles");
        }

        // ================= ADMIN ACCOUNTS =================
        public async Task<IActionResult> AdminAccounts()
        {
            if (RequirePermission("AdminAccounts", "View") is IActionResult redirect) return redirect;

            var accounts = await _db.Admins.Include(a => a.Role).OrderBy(a => a.Id).ToListAsync();
            return View(accounts);
        }

        [HttpGet]
        public async Task<IActionResult> AdminAccountCreate()
        {
            if (RequirePermission("AdminAccounts", "Create") is IActionResult redirect) return redirect;

            ViewBag.Roles = await _db.Roles.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminAccountCreate(string username, string password, string fullName, int roleId)
        {
            if (RequirePermission("AdminAccounts", "Create") is IActionResult redirect) return redirect;

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
            LogActivity("AdminAccounts", "Thêm tài khoản admin", "Đã thêm tài khoản admin mới!");
            TempData["Success"] = "Đã thêm tài khoản admin mới!";
            return RedirectToAction("AdminAccounts");
        }

        [HttpGet]
        public async Task<IActionResult> AdminAccountEdit(int id)
        {
            if (RequirePermission("AdminAccounts", "Edit") is IActionResult redirect) return redirect;

            var admin = await _db.Admins.FindAsync(id);
            if (admin == null) return NotFound();

            ViewBag.Roles = await _db.Roles.ToListAsync();
            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminAccountEdit(int id, string fullName, int roleId, string? newPassword)
        {
            if (RequirePermission("AdminAccounts", "Edit") is IActionResult redirect) return redirect;

            var admin = await _db.Admins.FindAsync(id);
            if (admin == null) return NotFound();

            admin.FullName = fullName;
            admin.RoleId = roleId;
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                admin.PasswordHash = HashPassword(newPassword);
            }

            await _db.SaveChangesAsync();
            LogActivity("AdminAccounts", "Sửa tài khoản admin", "Đã cập nhật tài khoản!");
            TempData["Success"] = "Đã cập nhật tài khoản!";
            return RedirectToAction("AdminAccounts");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminAccountDelete(int id)
        {
            if (RequirePermission("AdminAccounts", "Delete") is IActionResult redirect) return redirect;

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
                LogActivity("AdminAccounts", "Xóa tài khoản admin", "Đã xóa tài khoản admin!");
                TempData["Success"] = "Đã xóa tài khoản admin!";
            }
            return RedirectToAction("AdminAccounts");
        }

        // ================= CUSTOMERS (Клиенты) =================
        public async Task<IActionResult> Customers(string? searchTerm)
        {
            if (RequirePermission("Customers", "View") is IActionResult redirect) return redirect;

            var usersQuery = _db.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                usersQuery = usersQuery.Where(u => u.Name.Contains(searchTerm) || u.Email.Contains(searchTerm) || u.Phone.Contains(searchTerm));
            }
            var users = await usersQuery.OrderByDescending(u => u.CreatedAt).ToListAsync();

            var orderStats = await _db.Orders
                .Where(o => o.UserId != null)
                .GroupBy(o => o.UserId)
                .Select(g => new
                {
                    UserId = g.Key,
                    OrderCount = g.Count(),
                    TotalSpent = g.Sum(o => o.TotalPrice),
                    LastOrderAt = g.Max(o => o.CreatedAt)
                })
                .ToListAsync();

            var result = users.Select(u =>
            {
                var stat = orderStats.FirstOrDefault(s => s.UserId == u.Id);
                return new QuanLySach.ViewModels.CustomerViewModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Phone = u.Phone,
                    CreatedAt = u.CreatedAt,
                    OrderCount = stat?.OrderCount ?? 0,
                    TotalSpent = stat?.TotalSpent ?? 0,
                    LastOrderAt = stat?.LastOrderAt
                };
            }).OrderByDescending(c => c.TotalSpent).ToList();

            ViewBag.SearchTerm = searchTerm;
            return View(result);
        }

        public async Task<IActionResult> CustomerDetails(int id)
        {
            if (RequirePermission("Customers", "View") is IActionResult redirect) return redirect;

            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();

            var orders = await _db.Orders
                .Where(o => o.UserId == id)
                .Include(o => o.Items)
                .ThenInclude(i => i.Book)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            ViewBag.Customer = user;
            return View(orders);
        }

        // ================= STATISTICS (Статистика) =================
        public async Task<IActionResult> Statistics(DateTime? from, DateTime? to)
        {
            if (RequirePermission("Statistics", "View") is IActionResult redirect) return redirect;

            var toDate = (to ?? DateTime.Now).Date.AddDays(1).AddTicks(-1);
            var fromDate = (from ?? DateTime.Now.AddDays(-29)).Date;

            var orders = await _db.Orders
                .Where(o => o.CreatedAt >= fromDate && o.CreatedAt <= toDate)
                .Include(o => o.Items)
                .ThenInclude(i => i.Book)
                .ThenInclude(b => b!.Category)
                .ToListAsync();

            var vm = new QuanLySach.ViewModels.StatisticsViewModel
            {
                FromDate = fromDate,
                ToDate = toDate,
                TotalOrders = orders.Count,
                TotalRevenue = orders.Sum(o => o.TotalPrice),
                AverageOrderValue = orders.Count > 0 ? orders.Sum(o => o.TotalPrice) / orders.Count : 0,
                TotalBooksSold = orders.SelectMany(o => o.Items).Sum(i => i.Quantity)
            };

            vm.NewCustomers = await _db.Users.CountAsync(u => u.CreatedAt >= fromDate && u.CreatedAt <= toDate);

            var dayCount = (int)(toDate.Date - fromDate.Date).TotalDays + 1;
            for (int i = 0; i < dayCount; i++)
            {
                var day = fromDate.Date.AddDays(i);
                var revenue = orders.Where(o => o.CreatedAt.Date == day).Sum(o => o.TotalPrice);
                vm.RevenueByDay.Add((day.ToString("dd/MM"), revenue));
            }

            vm.RevenueByCategory = orders.SelectMany(o => o.Items)
                .Where(i => i.Book != null)
                .GroupBy(i => i.Book!.Category?.Name ?? "Khác")
                .Select(g => (g.Key, g.Sum(i => i.Price * i.Quantity), g.Sum(i => i.Quantity)))
                .OrderByDescending(x => x.Item2)
                .ToList();

            vm.OrdersByStatus = orders.GroupBy(o => o.Status)
                .Select(g => (g.Key, g.Count()))
                .OrderByDescending(x => x.Item2)
                .ToList();

            vm.TopBooks = orders.SelectMany(o => o.Items)
                .Where(i => i.Book != null)
                .GroupBy(i => i.Book!.Title)
                .Select(g => (g.Key, g.Sum(i => i.Quantity), g.Sum(i => i.Price * i.Quantity)))
                .OrderByDescending(x => x.Item2)
                .Take(10)
                .ToList();

            return View(vm);
        }
        // ================= MESSAGES (Сообщения от клиентов) =================
        public async Task<IActionResult> Messages()
        {
            if (RequirePermission("Messages", "View") is IActionResult redirect) return redirect;

            var messages = await _db.ContactMessages.OrderByDescending(m => m.CreatedAt).ToListAsync();

            // Đánh dấu tất cả tin nhắn chưa xem là đã xem (để tính lại badge)
            var unread = messages.Where(m => !m.IsRead).ToList();
            if (unread.Any())
            {
                foreach (var m in unread) m.IsRead = true;
                await _db.SaveChangesAsync();
            }

            return View(messages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MessageReply(int id, string reply)
        {
            if (RequirePermission("Messages", "Edit") is IActionResult redirect) return redirect;

            if (string.IsNullOrWhiteSpace(reply))
            {
                TempData["Error"] = "Vui lòng nhập nội dung trả lời!";
                return RedirectToAction("Messages");
            }

            var message = await _db.ContactMessages.FindAsync(id);
            if (message != null)
            {
                message.AdminReply = reply;
                message.RepliedAt = DateTime.Now;
                message.IsRead = true;
                message.IsReplyRead = false;
                await _db.SaveChangesAsync();
                LogActivity("Messages", "Trả lời tin nhắn", $"Đã trả lời tin nhắn của {message.Name}!");
                TempData["Success"] = "Đã gửi trả lời!";
            }
            return RedirectToAction("Messages");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MessageDelete(int id)
        {
            if (RequirePermission("Messages", "Delete") is IActionResult redirect) return redirect;

            var message = await _db.ContactMessages.FindAsync(id);
            if (message != null)
            {
                _db.ContactMessages.Remove(message);
                await _db.SaveChangesAsync();
                LogActivity("Messages", "Xóa tin nhắn", "Đã xóa tin nhắn liên hệ!");
                TempData["Success"] = "Đã xóa tin nhắn!";
            }
            return RedirectToAction("Messages");
        }

        // ================= SETTINGS (Настройки) =================
        public async Task<IActionResult> Settings()
        {
            if (RequirePermission("Settings", "View") is IActionResult redirect) return redirect;

            var settings = await _db.SiteSettings.FirstOrDefaultAsync();
            if (settings == null)
            {
                settings = new SiteSetting();
                _db.SiteSettings.Add(settings);
                await _db.SaveChangesAsync();
            }
            return View(settings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Settings(SiteSetting model)
        {
            if (RequirePermission("Settings", "Edit") is IActionResult redirect) return redirect;

            if (!ModelState.IsValid) return View(model);

            var settings = await _db.SiteSettings.FirstOrDefaultAsync();
            if (settings == null)
            {
                settings = new SiteSetting();
                _db.SiteSettings.Add(settings);
            }

            settings.StoreName = model.StoreName;
            settings.StoreEmail = model.StoreEmail;
            settings.StorePhone = model.StorePhone;
            settings.StoreAddress = model.StoreAddress;
            settings.FooterText = model.FooterText;
            settings.DefaultCurrency = model.DefaultCurrency;
            settings.ItemsPerPage = model.ItemsPerPage;
            settings.MaintenanceMode = model.MaintenanceMode;
            settings.MaintenanceMessage = model.MaintenanceMessage;
            settings.FacebookUrl = model.FacebookUrl;
            settings.InstagramUrl = model.InstagramUrl;
            settings.TelegramUrl = model.TelegramUrl;
            settings.UpdatedAt = DateTime.Now;

            await _db.SaveChangesAsync();
            LogActivity("Settings", "Cập nhật cài đặt", "Đã cập nhật cài đặt hệ thống!");
            TempData["Success"] = "Đã cập nhật cài đặt hệ thống!";
            return RedirectToAction("Settings");
        }

        // ================= PERMISSIONS (Права доступа) =================
        public async Task<IActionResult> Permissions(int? roleId)
        {
            if (RequirePermission("Permissions", "View") is IActionResult redirect) return redirect;

            var roles = await _db.Roles.OrderBy(r => r.Id).ToListAsync();
            if (!roles.Any())
            {
                TempData["Error"] = "Chưa có vai trò nào. Hãy tạo vai trò trước ở mục Роли.";
                return RedirectToAction("Roles");
            }

            var selectedId = roleId ?? roles.First().Id;
            var selectedRole = await _db.Roles.Include(r => r.Permissions).FirstOrDefaultAsync(r => r.Id == selectedId);
            if (selectedRole == null)
            {
                selectedRole = roles.First();
                selectedId = selectedRole.Id;
                selectedRole = await _db.Roles.Include(r => r.Permissions).FirstOrDefaultAsync(r => r.Id == selectedId);
            }

            var vm = new QuanLySach.ViewModels.PermissionsViewModel
            {
                AllRoles = roles,
                SelectedRoleId = selectedId,
                SelectedRole = selectedRole
            };

            foreach (var (key, label) in QuanLySach.ViewModels.PermissionsViewModel.Modules)
            {
                var existing = selectedRole?.Permissions.FirstOrDefault(p => p.Module == key);
                vm.Rows.Add(new QuanLySach.ViewModels.ModulePermissionRow
                {
                    Module = key,
                    DisplayName = label,
                    CanView = existing?.CanView ?? false,
                    CanCreate = existing?.CanCreate ?? false,
                    CanEdit = existing?.CanEdit ?? false,
                    CanDelete = existing?.CanDelete ?? false
                });
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PermissionsSave(int roleId, List<string> view, List<string> create, List<string> edit, List<string> delete)
        {
            if (RequirePermission("Permissions", "Edit") is IActionResult redirect) return redirect;

            var role = await _db.Roles.Include(r => r.Permissions).FirstOrDefaultAsync(r => r.Id == roleId);
            if (role == null) return NotFound();

            if (role.IsSuperAdmin)
            {
                TempData["Error"] = "Không thể chỉnh sửa quyền của vai trò siêu quản trị — vai trò này luôn có toàn quyền!";
                return RedirectToAction("Permissions", new { roleId });
            }

            view ??= new List<string>();
            create ??= new List<string>();
            edit ??= new List<string>();
            delete ??= new List<string>();

            foreach (var (key, _) in QuanLySach.ViewModels.PermissionsViewModel.Modules)
            {
                var existing = role.Permissions.FirstOrDefault(p => p.Module == key);
                if (existing == null)
                {
                    existing = new RolePermission { RoleId = role.Id, Module = key };
                    _db.RolePermissions.Add(existing);
                }
                existing.CanView = view.Contains(key);
                existing.CanCreate = create.Contains(key);
                existing.CanEdit = edit.Contains(key);
                existing.CanDelete = delete.Contains(key);
            }

            await _db.SaveChangesAsync();
            LogActivity("Permissions", "Cập nhật phân quyền", $"Đã cập nhật ma trận quyền cho vai trò \"{role.Name}\"!");
            TempData["Success"] = $"Đã cập nhật quyền cho vai trò \"{role.Name}\"!";
            return RedirectToAction("Permissions", new { roleId });
        }

        // ================= ACTIVITY LOG (Журнал действий) =================
        public async Task<IActionResult> ActivityLog(int page = 1, string? module = null, string? adminName = null)
        {
            if (RequirePermission("ActivityLog", "View") is IActionResult redirect) return redirect;

            const int pageSize = 25;
            var query = _db.ActivityLogs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(module))
                query = query.Where(l => l.Module == module);

            if (!string.IsNullOrWhiteSpace(adminName))
                query = query.Where(l => l.AdminName.Contains(adminName));

            var total = await query.CountAsync();
            var logs = await query
                .OrderByDescending(l => l.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(total / (double)pageSize);
            ViewBag.Module = module;
            ViewBag.AdminName = adminName;
            ViewBag.AllModules = await _db.ActivityLogs.Select(l => l.Module).Distinct().OrderBy(m => m).ToListAsync();

            return View(logs);
        }
    }
}