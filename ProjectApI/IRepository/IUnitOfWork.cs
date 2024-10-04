using ProjectApI.IRepository;

namespace ProjectAPI.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepo  CategoryRepo { get; }
        IProductRepo productRepo { get; }
        ICartRepo CartRepo { get; }
        IOrderRepo OrderRepo { get; }
        IPaymentRepo PaymentRepo { get; }  
        IUserRepo UserRepo { get; } 
        IWishListRepo WishListRepo { get; }
        int Complete();

    }
}
