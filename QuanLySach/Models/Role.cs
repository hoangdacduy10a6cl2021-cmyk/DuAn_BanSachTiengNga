using System.ComponentModel.DataAnnotations;

namespace QuanLySach.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        public List<Admin> Admins { get; set; } = new();
    }
}