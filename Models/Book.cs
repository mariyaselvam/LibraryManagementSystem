
namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }  // Primary key
        public string Title { get; set; }  // Title of the book
        public int AuthorId { get; set; }  // Foreign key referencing the Author model
        public string ISBN { get; set; }  // ISBN of the book
        public DateTime PublishDate { get; set; }  // Publication date of the book
        public Author Author { get; set; }
    }
}
