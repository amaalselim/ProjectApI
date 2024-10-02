using System.Text.Json.Serialization;

namespace ProjectApI.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product>? Products { get; set; }  

    }
}
