using ProjectApI.Data;
using ProjectApI.IRepository;
using ProjectApI.Repository;
using ProjectAPI.IRepository;

namespace ProjectAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;
        public ICategoryRepo CategoryRepo {  get; private set; }    

        public IProductRepo productRepo { get; private set; }

        public ICartRepo CartRepo { get; private set; }

        public IOrderRepo OrderRepo { get; private set; }

        public IPaymentRepo PaymentRepo { get; private set; }

        public IUserRepo UserRepo { get; private set; }

        public IWishListRepo WishListRepo { get; private set; }
        public UnitOfWork(Context context)
        {
            _context = context;
            CategoryRepo = new CategoryRepo(context);
            productRepo = new ProductRepo(context);
            CartRepo = new CartRepo(context);
            OrderRepo = new OrderRepo(context);
            PaymentRepo = new PaymentRepo(context);
            UserRepo = new UserRepo(context);
            WishListRepo = new WishListRepo(context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
