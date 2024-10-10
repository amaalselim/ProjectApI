using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectApI.Data;
using ProjectApI.IRepository;
using ProjectApI.Models;

namespace ProjectApI.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly Context _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public OrderRepo(Context context , UserManager<User> userManager,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

        }
        [Authorize]
        public async Task AddOrderAsync(Order order)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null) {
                throw new InvalidOperationException("User is not authenticated.");
            }
            order.UserId = user.Id;
            _context.orders.Add(order);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.orders.FindAsync(id);
            if (order != null)
            {
                _context.orders.Remove(order);
                await _context.SaveChangesAsync();  
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.orders.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.orders.FindAsync(id);
        }

        public async Task UpdateOrderAsync(int id, Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();  
        }
    }
}
