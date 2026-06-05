using QuanLySach.Models;

namespace QuanLySach.ViewModels
{
    public class HomeViewModel
    {
        public List<Book> PopularBooks { get; set; } = new();
        public List<Book> NewBooks { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
    }
}