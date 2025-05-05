using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [StringLength(13)]
        public string ISBN { get; set; }

        public DateTime PublishedDate { get; set; }

        // Foreign Keys
        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        [ForeignKey("Genre")]
        public int GenreId { get; set; }

        public int AvailableCopies { get; set; }

        // Navigation Properties
        public Author Author { get; set; }
        public Genre Genre { get; set; }

        public List<BorrowRecord> BorrowRecords { get; set; } = new();
    }
}
