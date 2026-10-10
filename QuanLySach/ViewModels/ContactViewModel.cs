using System.ComponentModel.DataAnnotations;

namespace QuanLySach.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Пожалуйста, введите ваше имя")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Пожалуйста, введите e-mail")]
        [EmailAddress(ErrorMessage = "Некорректный e-mail")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Пожалуйста, введите сообщение")]
        public string Message { get; set; } = "";

        // Lịch sử tin nhắn của người dùng hiện tại (nếu đã đăng nhập) + phản hồi của admin
        public List<QuanLySach.Models.ContactMessage> MyMessages { get; set; } = new();
    }
}