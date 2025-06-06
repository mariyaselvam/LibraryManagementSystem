namespace LibraryManagementSystem.DTOs.Book
{
    public class BookResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; } // Instead of AuthorDto, we just store the AuthorName
        public string GenreName { get; set; }
        public string ISBN { get; set; }
        public int AvailableCopies { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
