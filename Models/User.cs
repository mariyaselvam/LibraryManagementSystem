namespace LibraryManagementSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public UserRole Role { get; set; }
    }

    public enum UserRole
    {
        Admin,
        Staff
    }
}
