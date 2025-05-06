// Services/Implementations/ReportRepository.cs
using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Reports;
using LibraryManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services.Implementations
{
    public class ReportRepository : IReportRepository
    {
        private readonly LibraryDbContext _context;

        public ReportRepository(LibraryDbContext context)
        {
            _context = context;

        }

        public async Task<List<TopUserDto>> GetTopActiveUsersAsync()
        {
            var result = await _context.BorrowRecords
                .Include(b => b.User) // Make sure User is loaded
                .Where(b => b.User != null) // Filter out null Users
                .GroupBy(b => new { b.UserId, b.User.UserName, b.User.Email })
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => new TopUserDto
                {
                    UserName = g.Key.UserName ?? "Unknown",
                    Email = g.Key.Email ?? "Unknown",
                    BorrowCount = g.Count()
                })
                .ToListAsync();

            return result;
        }


        public async Task<List<BorrowingTrendDto>> GetBorrowingTrendsAsync()
        {
            var sixMonthsAgo = DateTime.Now.AddMonths(-5); // includes current month and previous 5 months

            var borrowData = await _context.BorrowRecords
                .Where(b => b.BorrowDate >= sixMonthsAgo)
                .GroupBy(b => new { b.BorrowDate.Year, b.BorrowDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Count = g.Count()
                })
                .ToListAsync();

            // Generate complete list of last 6 months
            var result = Enumerable.Range(0, 6)
                .Select(i =>
                {
                    var date = DateTime.Now.AddMonths(-i);
                    var data = borrowData.FirstOrDefault(x => x.Year == date.Year && x.Month == date.Month);
                    return new BorrowingTrendDto
                    {
                        Month = date.ToString("MMMM"),
                        BorrowCount = data?.Count ?? 0
                    };
                })
                .Reverse() // so it's in chronological order
                .ToList();

            return result;
        }




    }
}
