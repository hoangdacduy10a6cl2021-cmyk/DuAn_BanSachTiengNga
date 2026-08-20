namespace QuanLySach.ViewModels
{
    public class StatisticsViewModel
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageOrderValue { get; set; }
        public int TotalBooksSold { get; set; }
        public int NewCustomers { get; set; }

        public List<(string Label, decimal Value)> RevenueByDay { get; set; } = new();
        public List<(string CategoryName, decimal Revenue, int Qty)> RevenueByCategory { get; set; } = new();
        public List<(string Status, int Count)> OrdersByStatus { get; set; } = new();
        public List<(string BookTitle, int Qty, decimal Revenue)> TopBooks { get; set; } = new();
    }
}
