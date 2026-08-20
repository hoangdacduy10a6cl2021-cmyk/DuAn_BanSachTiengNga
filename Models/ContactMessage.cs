using System.ComponentModel.DataAnnotations;

namespace QuanLySach.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        // Nếu người gửi đã đăng nhập thì lưu UserId để hiện lại tin nhắn/phản hồi cho đúng tài khoản
        public int? UserId { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Email { get; set; } = "";

        [Required]
        public string Message { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Admin đã xem tin nhắn này chưa (để tính badge "tin nhắn mới" bên admin)
        public bool IsRead { get; set; } = false;

        // Nội dung admin trả lời
        public string? AdminReply { get; set; }
        public DateTime? RepliedAt { get; set; }

        // User đã xem phản hồi chưa (để tính badge "có phản hồi mới" bên trang người dùng)
        public bool IsReplyRead { get; set; } = false;
    }
}