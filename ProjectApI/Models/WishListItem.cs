using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApI.Models
{
    public class WishListItem
    {
        public int Id { get; set; }
        [ForeignKey("product")]
        public int ProductId { get; set; }
        public Product? product { get; set; }
    }
}
