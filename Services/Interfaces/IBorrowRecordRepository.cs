using LibraryManagementSystem.DTOs.Borrow;
using LibraryManagementSystem.DTOs.Reports;

public interface IBorrowRecordRepository
{
    Task<BorrowReadDto> BorrowBookAsync(string userId, int bookId);
    Task<bool> ReturnBookAsync(int borrowId);
    Task<IEnumerable<BorrowReadDto>> GetUserBorrowHistoryAsync(string userId);
    //Task<List<TopBookDto>> GetTopBorrowedBooksAsync();
    Task<List<TopBookDto>> GetTopBorrowedBooksAsync();


}
