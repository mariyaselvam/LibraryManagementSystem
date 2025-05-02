using LibraryManagementSystem.Models;
using System.Text.Json.Serialization;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }

    [JsonIgnore]
    public List<Book> Books { get; set; } // Ignore this property during serialization
}
