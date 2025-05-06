using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public UserRole Role { get; set; } // This stores the role of the user (Admin, Staff, User)

        public List<BorrowRecord> BorrowRecords { get; set; } = new();
    }


    public enum UserRole
    {
        Admin,
        Staff,
        User
    }

}
