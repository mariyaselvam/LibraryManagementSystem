using LibraryManagementSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Author
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    public DateTime DateOfBirth { get; set; }

    [JsonIgnore]
    public List<Book> Books { get; set; } = new();

    public bool IsDeleted { get; set; } = false; // ? Soft delete flag
}
