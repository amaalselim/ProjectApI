using ProjectApI.Models;

namespace ProjectApI.IRepository
{
    public interface IOrderRepo
    {
        public Task<IEnumerable<Order>> GetAllOrdersAsync();
        public Task<Order> GetOrderByIdAsync(int id);   
        public Task AddOrderAsync(Order order); 
        public Task DeleteOrderAsync(int id);   
        public Task UpdateOrderAsync(int id , Order order); 
    }
}
