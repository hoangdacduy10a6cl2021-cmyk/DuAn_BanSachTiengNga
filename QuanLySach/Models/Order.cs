namespace QuanLySach.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Новый";

        // Liên hệ
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";

        // Địa chỉ
        public string City { get; set; } = "";
        public string Street { get; set; } = "";
        public string House { get; set; } = "";
        public string Apartment { get; set; } = "";
        public string PostalCode { get; set; } = "";

        // Giao hàng
        public string DeliveryMethod { get; set; } = "Курьером";
        public decimal DeliveryPrice { get; set; } = 0;
        public decimal TotalPrice { get; set; }

        // Thanh toán
        public string PaymentMethod { get; set; } = "Наличными";

        public List<OrderItem> Items { get; set; } = new();
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}