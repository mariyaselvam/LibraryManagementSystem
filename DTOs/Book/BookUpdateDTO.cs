public class BookUpdateDTO
{
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }  // ? Add this
    public string ISBN { get; set; }
    public int AvailableCopies { get; set; }
    public DateTime PublishDate { get; set; }
}
