using ProjectApI.Models;

namespace ProjectApI.IRepository
{
    public interface IProductRepo
    {
        public Task<IEnumerable<Product>> GetProductAsync();
        public Task<Product> GetproductByIdAsync(int id);
        public Task CreateAsync(Product product);
        public Task UpdateAsync(Product product);
        public Task DeleteAsync(int id);
    }
}
