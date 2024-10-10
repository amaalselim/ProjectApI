using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectApI.Models;
using ProjectAPI.IRepository;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _unitOfWork.OrderRepo.GetAllOrdersAsync();
            return Ok(orders);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByOrderId(int id)
        {
            var orders = await _unitOfWork.OrderRepo.GetOrderByIdAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostOrder(Order order)
        {
            await _unitOfWork.OrderRepo.AddOrderAsync(order);
            return CreatedAtAction("GetByOrderId", new { id = order.Id }, order);
        }
        [HttpPut]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }
            try
            {
                await _unitOfWork.OrderRepo.UpdateOrderAsync(id, order);

            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _unitOfWork.OrderRepo.GetOrderByIdAsync(id).ConfigureAwait(false);
            if (order == null)
            {
                return NotFound();
            }
            await _unitOfWork.OrderRepo.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
