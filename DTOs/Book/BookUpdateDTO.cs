namespace LibraryManagementSystem.DTOs.Book
{
    public class BookUpdateDTO
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }  // Foreign Key to Author
        public string ISBN { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
