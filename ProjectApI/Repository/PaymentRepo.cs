using Microsoft.AspNetCore.Identity;
using ProjectApI.Data;
using ProjectApI.IRepository;
using ProjectApI.Models;

namespace ProjectApI.Repository
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly Context _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;

        public PaymentRepo(Context context,IHttpContextAccessor httpContextAccessor,UserManager<User> userManager)
        {
            _context = context;
            _contextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await _context.payments.FindAsync(id);
        }

        public async Task<Payment> ProcessPaymentAsync(Payment payment)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            payment.UserId = user.Id;
            await _context.payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }
    }
}
