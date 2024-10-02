using ProjectApI.Models;

namespace ProjectApI.IRepository
{
    public interface ICartRepo
    {
        public Task<Cart> GetCartByUserIdAsync (string userId);
        public Task CreateCartAsync (Cart cart);    
        public Task UpdateCartAsync (Cart cart);    
        public Task RemoveCartItemAsync (string UserId ,int ProductId);    
    }
}
