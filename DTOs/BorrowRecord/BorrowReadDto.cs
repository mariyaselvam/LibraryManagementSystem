namespace LibraryManagementSystem.DTOs.Borrow
{
    public class BorrowReadDto
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
        public decimal? LateFee { get; set; }
    }
}
