using System.ComponentModel.DataAnnotations;

namespace QuanLySach.Models
{
    public class Admin
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = "";

        [Required]
        public string PasswordHash { get; set; } = "";

        public string FullName { get; set; } = "";

        public int RoleId { get; set; }
        public Role? Role { get; set; }
    }
}