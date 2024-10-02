namespace ProjectApI.Models
{
    public class WishList
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<WishList>? wishLists { get; set; } = new List<WishList>();   

    }
}
