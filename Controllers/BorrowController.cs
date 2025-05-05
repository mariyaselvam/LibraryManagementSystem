using LibraryManagementSystem.DTOs.Borrow;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Interfaces; // ?
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagementSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowRecordRepository _borrowRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public BorrowController(IBorrowRecordRepository borrowRepo, UserManager<ApplicationUser> userManager)
        {
            _borrowRepo = borrowRepo;
            _userManager = userManager;
        }

        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowCreateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _borrowRepo.BorrowBookAsync(userId, dto.BookId);
            return Ok(result);
        }

        [HttpPost("return/{id}")]
        public async Task<IActionResult> ReturnBook(int id)
        {
            var success = await _borrowRepo.ReturnBookAsync(id);
            if (!success) return NotFound();
            return Ok();
        }

        [HttpGet("my-history")]
        public async Task<IActionResult> GetMyBorrowHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var history = await _borrowRepo.GetUserBorrowHistoryAsync(userId);
            return Ok(history);
        }
    }

}
