using LibraryManagementSystem.Services.Interfaces;
using LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        // Constructor to inject IBookRepository
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET: api/books
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            return Ok(books);
        }

        // POST: api/books
        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookCreateDTO bookCreateDTO)
        {
            var book = new Book
            {
                Title = bookCreateDTO.Title,
                AuthorId = bookCreateDTO.AuthorId,
                ISBN = bookCreateDTO.ISBN,
                PublishDate = bookCreateDTO.PublishDate
            };

            var createdBook = await _bookRepository.AddAsync(book);
            return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
        }

        // GET: api/books/{id}
        [HttpGet("{id}")]
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
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookUpdateDTO bookUpdateDTO)
        {
            var existingBook = await _bookRepository.GetByIdAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            existingBook.Title = bookUpdateDTO.Title;
            existingBook.ISBN = bookUpdateDTO.ISBN;
            existingBook.PublishDate = bookUpdateDTO.PublishDate;

            var updatedBook = await _bookRepository.UpdateAsync(existingBook);

            return Ok(updatedBook);
        }

        // DELETE: api/books/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var isDeleted = await _bookRepository.DeleteAsync(id);

            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Failed to delete the book.");
            }
        }
    }
}
