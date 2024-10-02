using Microsoft.EntityFrameworkCore;
using ProjectApI.Data;
using ProjectApI.IRepository;
using ProjectApI.Models;

namespace ProjectApI.Repository
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly Context _context;
        public CategoryRepo(Context context)
        {
            _context = context;
        }
        public async Task CreateAsync(Category category)
        {
            await _context.categories.AddAsync(category);  
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await _context.categories.SingleOrDefaultAsync(x=> x.Id == id); 
            _context.categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoryAsync()
        {
           List<Category> categories= await _context.categories.ToListAsync();
            return categories;  
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            Category category = await _context.categories.SingleOrDefaultAsync(x=>x.Id==id);
            return category;
        }

        public async Task UpdateAsync(Category category)
        {
            _context.categories.Update(category);
            await _context.SaveChangesAsync();  
        }
    }
}
