using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLySach.Models;

namespace QuanLySach.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _db;

        public BooksController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(int? categoryId, string sort = "popular", int page = 1, int pageSize = 8)
        {
            var query = _db.Books.Include(b => b.Category).AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(b => b.CategoryId == categoryId);

            query = sort switch
            {
                "price_asc" => query.OrderBy(b => b.Price),
                "price_desc" => query.OrderByDescending(b => b.Price),
                "new" => query.OrderByDescending(b => b.Id),
                _ => query.OrderByDescending(b => b.IsPopular)
            };

            int total = await query.CountAsync();
            var books = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var categories = await _db.Categories.ToListAsync();

            ViewBag.Categories = categories;
            ViewBag.CurrentCategory = categoryId;
            ViewBag.Sort = sort;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((double)total / pageSize);

            // Wishlist IDs cho user đang đăng nhập
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId != null)
            {
                var wishlistIds = _db.Wishlists
                    .Where(w => w.UserId == userId)
                    .Select(w => w.BookId)
                    .ToList();
                ViewBag.WishlistIds = wishlistIds;
            }
            else
            {
                ViewBag.WishlistIds = new List<int>();
            }

            return View(books);
        }

        public IActionResult ComingSoon()
        {
            return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            var book = await _db.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();

            // Sách liên quan cùng category
            var related = await _db.Books
                .Where(b => b.CategoryId == book.CategoryId && b.Id != id)
                .Take(4)
                .ToListAsync();

            ViewBag.RelatedBooks = related;

            // Wishlist
            var userId = HttpContext.Session.GetInt32("UserId");
            ViewBag.IsWishlisted = userId != null &&
                _db.Wishlists.Any(w => w.UserId == userId && w.BookId == id);

            return View(book);
        }
    }
}