using QuanLySach.Models;

namespace QuanLySach.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int TotalBooks { get; set; }
        public int TotalCategories { get; set; }
        public int TotalUsers { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<Order> RecentOrders { get; set; } = new();
    }
}