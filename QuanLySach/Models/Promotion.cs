using System.ComponentModel.DataAnnotations;

namespace QuanLySach.Models
{
    public class Promotion
    {
        public int Id { get; set; }

        [Required]
        public string Code { get; set; } = "";

        public string Description { get; set; } = "";

        [Range(1, 100)]
        public int DiscountPercent { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(7);

        public bool IsActive { get; set; } = true;

        // Tính trạng thái hiển thị dựa trên ngày (không lưu DB)
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string ComputedStatus
        {
            get
            {
                if (!IsActive) return "Отключено";
                var now = DateTime.Now;
                if (now < StartDate) return "Скоро начнётся";
                if (now > EndDate) return "Истекло";
                return "Активна";
            }
        }
    }
}