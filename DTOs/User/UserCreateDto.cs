using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTOs.User
{
    public class UserCreateDto
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;
    }
}
