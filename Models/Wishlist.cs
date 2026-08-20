namespace QuanLySach.Models
{
    public class Wishlist
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Book? Book { get; set; }
    }
}