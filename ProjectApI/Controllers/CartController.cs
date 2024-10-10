using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectApI.Models;
using ProjectAPI.DTO;
using ProjectAPI.IRepository;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public CartController(IUnitOfWork unitOfWork, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCart()
        {
            User user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var cart = await _unitOfWork.CartRepo.GetCartByUserIdAsync(user.Id);
            if (cart == null)
            {
                return NotFound("Cart Not Found");
            }
            return Ok(cart);
        }

        [HttpPost("add-item")]
        [Authorize]

        public async Task<IActionResult> AddToCart(CartItemDTO cartItemDTO)
        {
            var Product = await _unitOfWork.productRepo.GetproductByIdAsync(cartItemDTO.ProductId);
            if (Product == null)
            {
                return NotFound("Product Not Found");
            }
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var cart = await _unitOfWork.CartRepo.GetCartByUserIdAsync(user.Id);
            if (cart == null)
            {
                cart = new Cart()
                {
                    UserId = user.Id
                };
                await _unitOfWork.CartRepo.CreateCartAsync(cart);
            }
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == Product.Id);
            if (existingItem != null)
            {
                existingItem.quantity += cartItemDTO.Quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = Product.Id,
                    product = Product,
                    quantity = cartItemDTO.Quantity
                });
            }
            await _unitOfWork.CartRepo.UpdateCartAsync(cart);
            return Ok("Item Add To Cart");

        }
        [HttpDelete("remove-item/{productId:int}")]
        [Authorize]
        public async Task<IActionResult> RemoveFromCartItem(int productId)
        {
            User user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var cart = await _unitOfWork.CartRepo.GetCartByUserIdAsync(user.Id);
            if (cart == null)
            {
                return NotFound("Cart not Found");
            }
            await _unitOfWork.CartRepo.RemoveCartItemAsync(user.Id, productId);
            return Ok("Item Removed From Cart");
        }
    }
}
