using LibraryManagementSystem.Models;

public interface IBorrowRecordRepository
{
    Task<BorrowReadDto> BorrowBookAsync(string userId, int bookId); // Change return type to BorrowReadDto
    Task<bool> ReturnBookAsync(int borrowRecordId);
    Task<IEnumerable<BorrowRecord>> GetUserBorrowHistoryAsync(string userId);
}
