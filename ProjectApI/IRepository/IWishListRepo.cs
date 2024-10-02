using ProjectApI.DTO;
using ProjectApI.Models;

namespace ProjectApI.IRepository
{
    public interface IWishListRepo
    {
        public Task AddToWishListAsync(int productId);
        public Task RemoveFromWishListAsync(int productId);
        public Task<WishListDTO> GetWishListByUserIdAsync();
    }
}
