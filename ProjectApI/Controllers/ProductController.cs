using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectApI.Models;
using ProjectAPI.DTO;
using ProjectAPI.IRepository;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var product=await _unitOfWork.productRepo.GetProductAsync();
            return Ok(product);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProdById(int id)
        {
            var product=await _unitOfWork.productRepo.GetproductByIdAsync(id);
            GeneralResponse generalResponse = new GeneralResponse();
            if (product != null)
            {
                generalResponse.Success = true;
                generalResponse.Data= product;
            }
            else
            {
                generalResponse.Success = false;
                generalResponse.Data = "Invalid ID";
            }
            return Ok(product); 
        }
        [HttpPost]
        [Authorize("Admin")]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.productRepo.CreateAsync(product);
                return CreatedAtAction("GetProdById", new { id = product.Id }, product);
            }

            return BadRequest(ModelState);
        }
        [HttpPut("{id:int}")]
        [Authorize("Admin")]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.productRepo.UpdateAsync(product);
                return StatusCode(StatusCodes.Status204NoContent, "Data Saved");
            }
            return BadRequest("Data Not Valid");
        }
        [HttpDelete("{id:int}")]
        [Authorize("Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.productRepo.GetproductByIdAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            await _unitOfWork.productRepo.DeleteAsync(id);
            return StatusCode(StatusCodes.Status204NoContent, "Data Saved");
        }
        [HttpPost("Search")]
        public async Task<IActionResult> SearchProducts(ProductDTO productDTO)
        {
            var products = (await _unitOfWork.productRepo.GetProductAsync()).ToList(); // Convert to list

            if (!string.IsNullOrEmpty(productDTO.Keyword))
            {
                products = products.Where(p =>
                    p.Name.Contains(productDTO.Keyword, StringComparison.OrdinalIgnoreCase) ||
                    p.Description.Contains(productDTO.Keyword, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(productDTO.category))
            {
                products = products.Where(p => p.Category != null && p.Category.Name.Equals(productDTO.category)).ToList();
            }

            if (productDTO.minPrice > 0)
            {
                products = products.Where(p => p.Price >= productDTO.minPrice).ToList();
            }

            if (productDTO.maxPrice > 0)
            {
                products = products.Where(p => p.Price <= productDTO.maxPrice).ToList();
            }

            return Ok(products);
        }

    }
}
