using System.ComponentModel.DataAnnotations;

namespace QuanLySach.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Vui lòng nhập nội dung tin nhắn")]
        public string Message { get; set; } = "";

        // Lịch sử tin nhắn của người dùng hiện tại (nếu đã đăng nhập) + phản hồi của admin
        public List<QuanLySach.Models.ContactMessage> MyMessages { get; set; } = new();
    }
}