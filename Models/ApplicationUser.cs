using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        // Navigation property if you want to track borrow records
        public ICollection<BorrowRecord> BorrowRecords { get; set; }
    }
}
