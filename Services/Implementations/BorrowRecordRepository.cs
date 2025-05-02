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
        _mapper = mapper;  // Add IMapper here
    }

    public async Task<BorrowReadDto> BorrowBookAsync(string userId, int bookId)
    {
        var borrowRecord = new BorrowRecord
        {
            UserId = userId,
            BookId = bookId,
            BorrowDate = DateTime.UtcNow
        };

        _context.BorrowRecords.Add(borrowRecord);
        await _context.SaveChangesAsync();

        // Re-fetch the record including the Book
        var savedRecord = await _context.BorrowRecords
            .Include(b => b.Book)
            .FirstOrDefaultAsync(b => b.Id == borrowRecord.Id);

        // Map the saved record to BorrowReadDto and return
        return _mapper.Map<BorrowReadDto>(savedRecord);
    }

    public async Task<bool> ReturnBookAsync(int borrowRecordId)
    {
        var record = await _context.BorrowRecords.FindAsync(borrowRecordId);
        if (record == null || record.IsReturned)
            throw new Exception("Book not found.");

        record.IsReturned = true;
        record.ReturnDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<BorrowRecord>> GetUserBorrowHistoryAsync(string userId)
    {
        return await _context.BorrowRecords
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.BorrowDate)
            .ToListAsync();
    }
}
