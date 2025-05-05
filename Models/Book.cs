using LibraryManagementSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Book
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    [StringLength(13)]
    public string ISBN { get; set; }

    public DateTime PublishedDate { get; set; }

    [ForeignKey("Author")]
    public int AuthorId { get; set; }

    [ForeignKey("Genre")]
    public int GenreId { get; set; }

    public int AvailableCopies { get; set; }

    // Soft delete flag
    public bool IsDeleted { get; set; } = false;

    public Author Author { get; set; }
    public Genre Genre { get; set; }

    public List<BorrowRecord> BorrowRecords { get; set; } = new();
}
