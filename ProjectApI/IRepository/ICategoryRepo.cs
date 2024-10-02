using ProjectApI.Models;

namespace ProjectApI.IRepository
{
    public interface ICategoryRepo
    {
        public Task<IEnumerable<Category>> GetCategoryAsync();
        public Task<Category> GetCategoryByIdAsync(int id); 
        public Task CreateAsync (Category category);    
        public Task UpdateAsync (Category category);    
        public Task DeleteAsync (int id);

    }
}
