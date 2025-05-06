using LibraryManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Linq;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,staff")] // Only Admins can access this
    public class ReportsController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBorrowRecordRepository _borrowRepo;
        private readonly IReportRepository _reportRepository;

        public ReportsController(IBookRepository bookRepository, IBorrowRecordRepository borrowRepo, IReportRepository reportRepository)
        {
            _bookRepository = bookRepository;
            _borrowRepo = borrowRepo;
            _reportRepository = reportRepository;
        }

        [HttpGet("top-books")]
        public async Task<IActionResult> GetTopBorrowedBooks()
        {
            var topBooks = await _borrowRepo.GetTopBorrowedBooksAsync();
            return Ok(topBooks);
        }

        // Endpoint for exporting books to CSV
        [HttpGet("books/export")]
        public async Task<IActionResult> ExportBooksToCsv([FromQuery] string format = "csv")
        {
            if (format.ToLower() != "csv")
                return BadRequest("Only CSV export is supported.");

            // Fetch all books
            var books = await _bookRepository.GetAllAsync();
            
            // Build the CSV string using StringBuilder
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("Title,ISBN,Published Date,Genre,Author Name"); // Header row

            // Loop through books and create the CSV rows
            foreach (var book in books)
            {
                csvBuilder.AppendLine($"\"{book.Title}\",\"{book.ISBN}\",\"{book.PublishedDate:yyyy-MM-dd}\",\"{book.Genre?.Name}\",\"{book.Author?.Name}\"");
            }

            // Convert StringBuilder to byte array for download
            var csvBytes = Encoding.UTF8.GetBytes(csvBuilder.ToString());

            // Return the CSV as a downloadable file with a dynamic filename
            return File(csvBytes, "text/csv", $"BooksReport_{DateTime.Now:yyyyMMddHHmmss}.csv");
        }

        // Endpoint for fetching top 5 active users
        [HttpGet("active-users")]
        public async Task<IActionResult> GetTopActiveUsers()
        {
            try
            {
                var users = await _reportRepository.GetTopActiveUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong: {ex.Message}");
            }
        }

        [HttpGet("borrowing-trends")]
        public async Task<IActionResult> GetBorrowingTrends()
        {
            try
            {
                var trends = await _reportRepository.GetBorrowingTrendsAsync();
                return Ok(trends);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong: {ex.Message}");
            }
        }



    }
}
