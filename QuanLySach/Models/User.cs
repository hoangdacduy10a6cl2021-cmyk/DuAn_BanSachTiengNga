using System.ComponentModel.DataAnnotations;

namespace QuanLySach.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Phone { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // ===== SETTINGS =====
        public bool ShowCyrillic { get; set; } = false;
        public bool ShowTransliteration { get; set; } = false;
        public bool EmailNotifications { get; set; } = true;
        public bool SmsNotifications { get; set; } = false;
        public string Language { get; set; } = "Вьетнам";
        public string Currency1 { get; set; } = "VND";
        public string Currency2 { get; set; } = "RUB";
        public string Currency3 { get; set; } = "USD";
    }
}