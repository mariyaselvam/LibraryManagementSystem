using LibraryManagementSystem.DTOs.Borrow;

public interface IBorrowRecordRepository
{
    Task<BorrowReadDto> BorrowBookAsync(string userId, int bookId);
    Task<bool> ReturnBookAsync(int borrowId);
    Task<IEnumerable<BorrowReadDto>> GetUserBorrowHistoryAsync(string userId);
}
