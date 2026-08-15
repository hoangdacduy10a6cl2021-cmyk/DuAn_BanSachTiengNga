using System.ComponentModel.DataAnnotations;

namespace QuanLySach.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        // Vai trò siêu quản trị: luôn có toàn quyền trên mọi module, không thể bị giới hạn hay xóa
        public bool IsSuperAdmin { get; set; } = false;

        public List<Admin> Admins { get; set; } = new();
        public List<RolePermission> Permissions { get; set; } = new();
    }
}