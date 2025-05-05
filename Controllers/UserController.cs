using LibraryManagementSystem.DTOs.User;
using LibraryManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Only authenticated users can access the controller
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        // ? Only Admins can view all users
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsersAsync();
            return Ok(users);
        }

        // ? Only Admins can create new users
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
        {
            var result = await _userRepo.CreateUserAsync(dto);
            if (result == null)
                return BadRequest("Username already exists.");

            return CreatedAtAction(nameof(GetAllUsers), new { id = result.Id }, result);
        }
    }
}
