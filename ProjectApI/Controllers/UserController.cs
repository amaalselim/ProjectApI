using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectApI.DTO;
using ProjectApI.Models;
using ProjectAPI.IRepository;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<User> _signInManager;

        public UserController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IUnitOfWork unitOfWork,SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                IdentityResult res = await _unitOfWork.UserRepo.Register(registerDTO);
                if (res.Succeeded)
                {
                    return Ok(res);
                }
                else
                {
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError("Error", error.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                object testlogin = await _unitOfWork.UserRepo.Login(loginDTO);
                if (testlogin != null)
                {
                    return Ok(testlogin);
                }

            }
            ModelState.AddModelError("Errors", "user name or password invalid");
            return BadRequest(ModelState);
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logged Out Successfully" });
        }
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole (string email,string role)
        {
            var user=await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User Not Found");
            }
            if(!await _roleManager.RoleExistsAsync(role))
            {
                var roleresult=await _roleManager.CreateAsync(new IdentityRole(role));
                if (!roleresult.Succeeded)
                {
                    return BadRequest("Failed To Create Role");
                }
            }
            await _userManager.AddToRoleAsync(user, role);
            return Ok("Role Assigned Successfully");
        }



    }
}
