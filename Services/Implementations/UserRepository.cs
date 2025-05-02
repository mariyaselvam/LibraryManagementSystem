using AutoMapper;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.User;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LibraryManagementSystem.Services.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(LibraryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<UserReadDto?> CreateUserAsync(UserCreateDto userCreateDto)
        {
            // Check if the username already exists
            if (await _context.Users.AnyAsync(u => u.Username == userCreateDto.Username))
                return null;

            // Create the User object
            var user = new User
            {
                Username = userCreateDto.Username,
                PasswordHash = HashPassword(userCreateDto.Password),
                Role = Enum.Parse<UserRole>(userCreateDto.Role, true)
            };

            // Add the new user to the database and save
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Map the created user to the DTO
            return _mapper.Map<UserReadDto>(user);
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }
    }
}
