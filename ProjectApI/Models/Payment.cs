namespace ProjectApI.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }= DateTime.Now;


    }
}
