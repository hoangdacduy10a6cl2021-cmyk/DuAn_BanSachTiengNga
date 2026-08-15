namespace QuanLySach.Models
{
    public class RolePermission
    {
        public int Id { get; set; }

        public int RoleId { get; set; }
        public Role? Role { get; set; }

        // Tên module: Books, Categories, Authors, Publishers, Orders, Users, Promotions,
        // Roles, AdminAccounts, Customers, Statistics, Settings, Permissions, ActivityLog
        public string Module { get; set; } = "";

        public bool CanView { get; set; } = false;
        public bool CanCreate { get; set; } = false;
        public bool CanEdit { get; set; } = false;
        public bool CanDelete { get; set; } = false;
    }
}
