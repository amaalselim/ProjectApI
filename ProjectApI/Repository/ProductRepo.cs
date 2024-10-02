using Microsoft.EntityFrameworkCore;
using ProjectApI.Data;
using ProjectApI.IRepository;
using ProjectApI.Models;

namespace ProjectApI.Repository
{
    public class ProductRepo : IProductRepo
    {
            private readonly Context _context;
            public ProductRepo(Context context)
            {
                _context = context;
            }
            public async Task CreateAsync(Product product)
            {
                await _context.products.AddAsync(product);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                Product product = await _context.products.SingleOrDefaultAsync(x => x.Id == id);
                _context.products.Remove(product);
                await _context.SaveChangesAsync();
            }


            public async Task<Product> GetproductByIdAsync(int id)
            {
                Product product = await _context.products.SingleOrDefaultAsync(x => x.Id == id);
                return product;
            }

            public async Task UpdateAsync(Product product)
            {
                _context.products.Update(product);
                await _context.SaveChangesAsync();
            }

            public async Task<IEnumerable<Product>> GetProductAsync()
            {
                List<Product> products = await _context.products.ToListAsync();
                return products;
            }
    }
}
