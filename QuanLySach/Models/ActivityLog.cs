namespace QuanLySach.Models
{
    public class ActivityLog
    {
        public int Id { get; set; }

        public int? AdminId { get; set; }
        public string AdminName { get; set; } = "";

        // Module bị tác động: Books, Orders, Users, Auth, ...
        public string Module { get; set; } = "";

        // Tên hành động ngắn gọn: "Thêm sách", "Xóa danh mục", "Đăng nhập", ...
        public string Action { get; set; } = "";

        // Mô tả chi tiết hành động
        public string Description { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string IpAddress { get; set; } = "";
    }
}
