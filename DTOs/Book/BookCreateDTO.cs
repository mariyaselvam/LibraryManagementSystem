namespace LibraryManagementSystem.DTOs.Book
{
    public class BookCreateDTO
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }  // Foreign key to Author
        public string ISBN { get; set; }
        public DateTime PublishDate { get; set; }  // Add PublishDate property
    }
}
