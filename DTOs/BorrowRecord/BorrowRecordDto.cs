using LibraryManagementSystem.Models;

public class BorrowRecordDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; } // Navigation property
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal? LateFee { get; set; }
    public bool IsReturned { get; set; }
}
