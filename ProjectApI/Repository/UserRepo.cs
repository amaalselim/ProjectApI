using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ProjectApI.Data;
using ProjectApI.DTO;
using ProjectApI.IRepository;
using ProjectApI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectApI.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly Context _context;
        private readonly UserManager<User> _manager;
        private readonly IConfiguration _config;

        public UserRepo(Context context)
        {
            _context = context;
        }
        public UserRepo(Context context,UserManager<User>manager,IConfiguration config)
        {
            _config = config;
            _context = context;
            _manager = manager; 
        }

        public async Task<object> Login(LoginDTO loginDTO)
        {
            var user = await _manager.FindByNameAsync(loginDTO.UserName);   
            if (user != null)
            {
                bool check = await _manager.CheckPasswordAsync(user, loginDTO.Password);
                if (check)
                {
                    List<Claim> claimss = new List<Claim>();
                    claimss.Add(new Claim(ClaimTypes.Name, loginDTO.UserName));
                    claimss.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    claimss.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    var roles = await _manager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        claimss.Add(new Claim(ClaimTypes.Role,role));   
                    }

                    SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
                    SigningCredentials signingCredentialss = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    JwtSecurityToken token = new JwtSecurityToken(
                            issuer: _config["JWT:Issuer"],
                            audience: _config["JWT:Audience"],
                            expires: DateTime.Now.AddHours(1),
                            claims:claimss,
                            signingCredentials:signingCredentialss
                            
                    );
                    return new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expire=DateTime.Now.AddHours(1)
                    };

                }
            }
            return new { };
        }

        public async Task<IdentityResult> Register(RegisterDTO registerDTO)
        {
            User user = new User();
            user.UserName = registerDTO.UserName;
            user.Email= registerDTO.Email;
            user.PasswordHash = registerDTO.Password;
            user.PhoneNumber = registerDTO.PhoneNumber;

            IdentityResult result = await _manager.CreateAsync(user, registerDTO.UserName);
            return result;

        }
    }
}
