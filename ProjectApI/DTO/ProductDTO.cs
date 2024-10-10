namespace ProjectAPI.DTO
{
    public class ProductDTO
    {
        public string Keyword { get; set; } 
        public decimal? minPrice { get; set; }
        public decimal? maxPrice { get; set; }
        public string category { get; set; }    
    }
}
