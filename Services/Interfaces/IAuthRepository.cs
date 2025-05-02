using LibraryManagementSystem.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Services.Interfaces
{
    public interface IAuthRepository
    {
        Task<IdentityResult> RegisterAsync(RegisterDTO dto);
        Task<string> LoginAsync(LoginDTO dto);
    }
}
