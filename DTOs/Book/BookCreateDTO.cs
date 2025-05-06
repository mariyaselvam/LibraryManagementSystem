using System.ComponentModel.DataAnnotations;

public class BookCreateDTO
{
    [Required]
    public string Title { get; set; }

    [Required]
    public int AuthorId { get; set; }

    [Required]
    public int GenreId { get; set; }

    [Required]
    [StringLength(13)]
    public string ISBN { get; set; }

    [Required]
    public int AvailableCopies { get; set; }

    [Required]
    public DateTime PublishedDate { get; set; }  // Match name with model
}
