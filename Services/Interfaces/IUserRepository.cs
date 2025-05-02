using LibraryManagementSystem.DTOs.User;

namespace LibraryManagementSystem.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
        Task<UserReadDto?> CreateUserAsync(UserCreateDto userCreateDto);
    }
}
