using LibraryManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.DTOs.Author;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorsController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var authors = await _authorRepository.GetAllAsync();
                return Ok(authors);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorCreateDTO authorCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Return validation errors
            }
            try
            {
                var author = new Author
                {
                    Name = authorCreateDTO.Name,
                    DateOfBirth = authorCreateDTO.DateOfBirth
                };

                var createdAuthor = await _authorRepository.AddAsync(author);
                return CreatedAtAction(nameof(GetById), new { id = createdAuthor.Id }, createdAuthor);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var author = await _authorRepository.GetByIdAsync(id);
                if (author == null)
                {
                    return NotFound();  // Return 404 if author doesn't exist
                }
                return Ok(author);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorUpdateDTO authorUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Return validation errors
            }

            try
            {
                var existingAuthor = await _authorRepository.GetByIdAsync(id);
                if (existingAuthor == null)
                {
                    return NotFound();  // Return 404 if author doesn't exist
                }

                existingAuthor.Name = authorUpdateDTO.Name;
                existingAuthor.DateOfBirth = authorUpdateDTO.DateOfBirth;

                var updatedAuthor = await _authorRepository.UpdateAsync(existingAuthor);
                return Ok(updatedAuthor);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var author = await _authorRepository.GetByIdAsync(id);
                if (author == null)
                {
                    return NotFound();  // Return 404 if author doesn't exist
                }

                var isDeleted = await _authorRepository.DeleteAsync(id);
                if (isDeleted)
                {
                    return NoContent();  // HTTP 204 No Content indicates successful deletion
                }
                else
                {
                    return BadRequest("Failed to delete the author.");
                }
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
