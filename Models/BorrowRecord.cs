using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class BorrowRecord
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required]
        public int BookId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        public DateTime BorrowDate { get; set; } = DateTime.UtcNow;

        public DateTime DueDate { get; set; } = DateTime.UtcNow.AddDays(14);

        public DateTime? ReturnDate { get; set; }

        public bool IsReturned { get; set; } = false;

        public decimal? LateFee { get; set; }
    }
}
