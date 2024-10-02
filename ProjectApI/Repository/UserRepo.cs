using Microsoft.AspNetCore.Identity;
using ProjectApI.DTO;
using ProjectApI.IRepository;

namespace ProjectApI.Repository
{
    public class UserRepo : IUserRepo
    {
        public Task<object> Login(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> Register(RegisterDTO registerDTO)
        {
            throw new NotImplementedException();
        }
    }
}
