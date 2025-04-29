using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTOs.Author
{
    public class AuthorCreateDTO
    {
        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The DateOfBirth field is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid Date format.")]
        public DateTime DateOfBirth { get; set; }
    }
}
