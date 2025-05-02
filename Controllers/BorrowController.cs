using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowRecordRepository _repo;
        private readonly IMapper _mapper;

        public BorrowController(IBorrowRecordRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> BorrowBook(BorrowCreateDto dto)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token");

            var record = await _repo.BorrowBookAsync(userId, dto.BookId);  // Change repo method to accept string
            return Ok(_mapper.Map<BorrowReadDto>(record));
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID not found in token");

            var records = await _repo.GetUserBorrowHistoryAsync(userId);  // Change repo method to accept string
            return Ok(_mapper.Map<IEnumerable<BorrowReadDto>>(records));
        }

    }

}
