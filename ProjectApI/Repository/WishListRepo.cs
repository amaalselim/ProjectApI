using ProjectApI.Data;
using ProjectApI.DTO;
using ProjectApI.IRepository;

namespace ProjectApI.Repository
{
    public class WishListRepo : IWishListRepo
    {
        private Context context;

        public WishListRepo(Context context)
        {
            this.context = context;
        }

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
