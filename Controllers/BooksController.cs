using LibraryManagementSystem.Services.Interfaces;
using LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Data;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // All actions require authentication
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly LibraryDbContext _context;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IMapper mapper, LibraryDbContext context)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _context = context;
        }

        // GET: api/books
        [HttpGet]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> GetAllBooks([FromQuery] bool includeDeleted = false)
        {
            var books = await _bookRepository.GetAllAsync(includeDeleted);
            var bookDtos = _mapper.Map<IEnumerable<BookResponseDTO>>(books);

            return Ok(bookDtos);
        }


        // POST: api/books
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBook([FromBody] BookCreateDTO bookCreateDTO)
        {
            // Check if Author exists
            var authorExists = await _context.Authors.AnyAsync(a => a.Id == bookCreateDTO.AuthorId);
            if (!authorExists)
            {
                return NotFound($"Author with ID {bookCreateDTO.AuthorId} not found.");
            }

            // Check if Genre exists
            var genreExists = await _context.Genres.AnyAsync(g => g.Id == bookCreateDTO.GenreId);
            if (!genreExists)
            {
                return NotFound($"Genre with ID {bookCreateDTO.GenreId} not found.");
            }

            // Create and save the book
            var book = _mapper.Map<Book>(bookCreateDTO);
            var createdBook = await _bookRepository.AddAsync(book);
            return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
        }


        // GET: api/books/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }


        // PUT: api/books/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookUpdateDTO bookUpdateDTO)
        {
            var existingBook = await _bookRepository.GetByIdAsync(id);
            if (existingBook == null) return NotFound();

            _mapper.Map(bookUpdateDTO, existingBook);
            var updatedBook = await _bookRepository.UpdateAsync(existingBook);
            return Ok(updatedBook);
        }

        // DELETE: api/books/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var isDeleted = await _bookRepository.DeleteAsync(id);
            return isDeleted ? NoContent() : BadRequest("Failed to delete the book.");
        }


    }


}
