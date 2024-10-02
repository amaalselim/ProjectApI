using ProjectApI.DTO;
using ProjectApI.IRepository;

namespace ProjectApI.Repository
{
    public class WishListRepo : IWishListRepo
    {
        public Task AddToWishListAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<WishListDTO> GetWishListByUserIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromWishListAsync(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
