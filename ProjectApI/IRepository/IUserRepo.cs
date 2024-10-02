using Microsoft.AspNetCore.Identity;
using ProjectApI.DTO;

namespace ProjectApI.IRepository
{
    public interface IUserRepo
    {
        public Task<Object> Login(LoginDTO loginDTO);
        public Task<IdentityResult> Register (RegisterDTO registerDTO);
    }
}
