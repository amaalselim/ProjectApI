using Microsoft.EntityFrameworkCore;
using ProjectApI.Data;
using ProjectApI.IRepository;
using ProjectApI.Models;

namespace ProjectApI.Repository
{
    public class CartRepo : ICartRepo
    {
        private readonly Context _context;
        public CartRepo(Context context)
        {
            _context = context;
        }
        public async Task CreateCartAsync(Cart cart)
        {
            await _context.carts.AddAsync(cart);    
            await _context.SaveChangesAsync();  
        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            return await _context.carts.Include(c => c.Items)
                .ThenInclude(i => i.product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task RemoveCartItemAsync(string UserId, int ProductId)
        {
            var cart = await GetCartByUserIdAsync(UserId);
            if (cart == null) return;
            var cartItem = cart.Items.FirstOrDefault(c => c.ProductId == ProductId);
            if(cartItem != null)
            {
                cart.Items.Remove(cartItem);
                await UpdateCartAsync(cart);  
            }
        }

        public async Task UpdateCartAsync(Cart cart)
        {
             _context.carts.Update(cart);
            await _context.SaveChangesAsync();
            

        }
    }
}
