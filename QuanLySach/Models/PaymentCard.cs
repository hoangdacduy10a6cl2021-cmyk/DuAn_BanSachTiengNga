namespace QuanLySach.Models
{
    public class PaymentCard
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CardNumber { get; set; } = "";
        public string CardName { get; set; } = "";
        public string Expiry { get; set; } = "";
        public string CardType { get; set; } = "VISA";
        public User? User { get; set; }
    }
}