using AutoMapper;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Borrow;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        var borrow = new BorrowRecord
        {
            UserId = userId,
            BookId = bookId
        };

        _context.BorrowRecords.Add(borrow);
        await _context.SaveChangesAsync();

        var borrowWithBook = await _context.BorrowRecords
            .Include(b => b.Book)
            .FirstOrDefaultAsync(b => b.Id == borrow.Id);

        return _mapper.Map<BorrowReadDto>(borrowWithBook);
    }

    public async Task<bool> ReturnBookAsync(int borrowId)
    {
        var borrow = await _context.BorrowRecords.FindAsync(borrowId);
        if (borrow == null || borrow.IsReturned) return false;

        borrow.IsReturned = true;
        borrow.ReturnDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<BorrowReadDto>> GetUserBorrowHistoryAsync(string userId)
    {
        var records = await _context.BorrowRecords
            .Where(r => r.UserId == userId)
            .Include(r => r.Book)
            .OrderByDescending(r => r.BorrowDate)
            .ToListAsync();

        return _mapper.Map<IEnumerable<BorrowReadDto>>(records);
    }
}
