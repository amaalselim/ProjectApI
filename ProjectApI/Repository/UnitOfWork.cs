using Microsoft.AspNetCore.Identity;
using ProjectApI.Data;
using ProjectApI.IRepository;
using ProjectApI.Models;
using ProjectApI.Repository;
using ProjectAPI.IRepository;

namespace ProjectAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;
        private readonly UserManager<User> _manager;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ICategoryRepo CategoryRepo {  get; private set; }    

        public IProductRepo productRepo { get; private set; }

        public ICartRepo CartRepo { get; private set; }

        public IOrderRepo OrderRepo { get; private set; }

        public IPaymentRepo PaymentRepo { get; private set; }

        public IUserRepo UserRepo { get; private set; }

        public IWishListRepo WishListRepo { get; private set; }
        public UnitOfWork(Context context, UserManager<User> manager, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _manager = manager;
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            CategoryRepo = new CategoryRepo(context);
            productRepo = new ProductRepo(context);
            CartRepo = new CartRepo(context);
            OrderRepo = new OrderRepo(context,manager,httpContextAccessor);
            PaymentRepo = new PaymentRepo(context,httpContextAccessor,manager);
            UserRepo = new UserRepo(context,manager,config);
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
