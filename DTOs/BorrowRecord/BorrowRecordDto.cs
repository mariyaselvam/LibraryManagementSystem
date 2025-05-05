namespace LibraryManagementSystem.DTOs.Borrow
{
    public class BorrowRecordDto
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        public string BookTitle { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public bool IsReturned { get; set; }
        public decimal? LateFee { get; set; }
    }
}
