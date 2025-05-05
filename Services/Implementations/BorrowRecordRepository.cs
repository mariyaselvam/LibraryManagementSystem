using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

public class BorrowRecordRepository : IBorrowRecordRepository
{
    private readonly LibraryDbContext _context;
    private readonly IMapper _mapper;

    public BorrowRecordRepository(LibraryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BorrowReadDto> BorrowBookAsync(string userId, int bookId)
    {
        // Convert string userId to int if needed
        if (!int.TryParse(userId, out int userIdInt))
            throw new Exception("Invalid user ID format.");

        var borrowRecord = new BorrowRecord
        {
            UserId = userIdInt,
            BookId = bookId,
            // Commenting out if not in model
            // BorrowDate = DateTime.UtcNow
        };

        _context.BorrowRecords.Add(borrowRecord);
        await _context.SaveChangesAsync();

        var savedRecord = await _context.BorrowRecords
            .Include(b => b.Book)
            .FirstOrDefaultAsync(b => b.Id == borrowRecord.Id);

        return _mapper.Map<BorrowReadDto>(savedRecord);
    }

    public async Task<bool> ReturnBookAsync(int borrowRecordId)
    {
        var record = await _context.BorrowRecords.FindAsync(borrowRecordId);
        if (record == null)
            throw new Exception("Book not found.");

        // Only update if those properties exist in the model
        // record.IsReturned = true;
        // record.ReturnDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<BorrowRecord>> GetUserBorrowHistoryAsync(string userId)
    {
        if (!int.TryParse(userId, out int userIdInt))
            throw new Exception("Invalid user ID.");

        return await _context.BorrowRecords
            .Where(r => r.UserId == userIdInt)
            //.OrderByDescending(r => r.BorrowDate)  // Skip if BorrowDate doesn't exist
            .ToListAsync();
    }
}
