namespace ProjectApI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string TotalPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime Orderdate { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public DateTime DateTime { get; set; }= DateTime.Now;
        public int OrderStatus { get; set; }
    }
}
