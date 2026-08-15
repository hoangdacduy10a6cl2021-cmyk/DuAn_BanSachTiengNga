using System.ComponentModel.DataAnnotations;

namespace QuanLySach.Models
{
    public class SiteSetting
    {
        public int Id { get; set; }

        [Required]
        public string StoreName { get; set; } = "Книжный Рай";

        public string StoreEmail { get; set; } = "";
        public string StorePhone { get; set; } = "";
        public string StoreAddress { get; set; } = "";

        public string FooterText { get; set; } = "";

        // Hiển thị / tiền tệ
        public string DefaultCurrency { get; set; } = "RUB";
        public int ItemsPerPage { get; set; } = 12;

        // Vận hành
        public bool MaintenanceMode { get; set; } = false;
        public string MaintenanceMessage { get; set; } = "Trang web đang bảo trì, vui lòng quay lại sau.";

        // Mạng xã hội
        public string FacebookUrl { get; set; } = "";
        public string InstagramUrl { get; set; } = "";
        public string TelegramUrl { get; set; } = "";

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
