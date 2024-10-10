using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectApI.Models;
using ProjectAPI.IRepository;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("Process")]
        [Authorize]

        public async Task<IActionResult> ProcessPayment(Payment payment)
        {
            var Processpayment = await _unitOfWork.PaymentRepo.ProcessPaymentAsync(payment);
            return Ok(Processpayment);
        }
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var payment = await _unitOfWork.PaymentRepo.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound("Payment Not Found");
            }
            return Ok(payment);
        }
        
    }
}
