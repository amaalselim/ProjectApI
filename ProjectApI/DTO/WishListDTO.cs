using ProjectApI.Models;

namespace ProjectApI.DTO
{
    public class WishListDTO
    {
        public string UserId { get; set; }  
        public int WishListId { get; set; }
        public ICollection<Product>? products {  get; set; }    
    }
}
