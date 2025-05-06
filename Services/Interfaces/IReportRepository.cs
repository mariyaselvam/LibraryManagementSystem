// Services/Interfaces/IReportRepository.cs
using LibraryManagementSystem.DTOs.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services.Interfaces
{
    public interface IReportRepository
    {
        Task<List<TopUserDto>> GetTopActiveUsersAsync();
        Task<List<BorrowingTrendDto>> GetBorrowingTrendsAsync();



    }
}
