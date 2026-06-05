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
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Promotions()
        {
            return View();
        }
        public IActionResult Popular()
        {
            var popularBooks = _db.Books
                .Where(b => b.IsPopular == true)
                .ToList();
            return View(popularBooks);
        }
        public IActionResult NewBooks()
        {
            return View();
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