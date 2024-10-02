using ProjectApI.Models;

namespace ProjectApI.IRepository
{
    public interface IPaymentRepo
    {
        public Task<Payment> ProcessPaymentAsync (Payment payment); 
        public Task<Payment> GetPaymentByIdAsync (int id);
    }
}
