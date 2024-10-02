using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApI.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int quantity { get; set; }
        [ForeignKey("product")]
        public int ProductId { get; set; }
        public product? product { get; set; }
    }
}
