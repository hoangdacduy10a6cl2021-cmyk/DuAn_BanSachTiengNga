using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySach.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public decimal Price { get; set; }
        public string CoverImageUrl { get; set; } = "";
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool IsNew { get; set; }
        public bool IsPopular { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; } = 0;

        // % giảm giá (0-100). 0 = sách không sale.
        public int DiscountPercent { get; set; } = 0;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [NotMapped]
        public bool HasDiscount => DiscountPercent > 0 && DiscountPercent < 100;

        [NotMapped]
        public decimal FinalPrice => HasDiscount
            ? Math.Round(Price * (100 - DiscountPercent) / 100m, 2)
            : Price;
    }
}