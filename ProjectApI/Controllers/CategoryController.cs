using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectApI.Data;
using ProjectApI.Models;
using ProjectAPI.DTO;
using ProjectAPI.IRepository;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Context _context;

        public CategoryController(IUnitOfWork unitOfWork, Context context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        [HttpGet("Count")]
        public ActionResult<List<CategoryWithProdDTO>> Get()
        {
            List<CategoryWithProdDTO> result= new List<CategoryWithProdDTO>();
            List<Category> categories = _context.categories.Include(c=>c.Products).ToList();
            foreach (var category in categories)
            {
                CategoryWithProdDTO categoryWithProdDTO = new CategoryWithProdDTO();    
                categoryWithProdDTO.Name = category.Name;
                categoryWithProdDTO.Id = category.Id;
                categoryWithProdDTO.ProductCount = category.Products.Count();
                result.Add(categoryWithProdDTO);
            }
            return result;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var Cat= await _unitOfWork.CategoryRepo.GetCategoryAsync();
            return Ok(Cat);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCatById(int id)
        {
            Category cat =await _unitOfWork.CategoryRepo.GetCategoryByIdAsync(id);
            if (cat == null)
            {
                return NotFound();
            }
            return Ok(cat);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.CategoryRepo.CreateAsync(category);
                return CreatedAtAction("GetCatById", new { id = category.Id }, category);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Edit(int id,Category category)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.CategoryRepo.UpdateAsync(category);
                return StatusCode(StatusCodes.Status204NoContent, "Data Saved");

            }
            return BadRequest("Data Not Valid");
        }
        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete (int id)
        {
            var cat = await _unitOfWork.CategoryRepo.GetCategoryByIdAsync(id);
            if(cat == null)
            {
                return NotFound();
            }
            await _unitOfWork.CategoryRepo.DeleteAsync(id);
            return StatusCode(StatusCodes.Status204NoContent, "Data Saved");


        }

    }
}
