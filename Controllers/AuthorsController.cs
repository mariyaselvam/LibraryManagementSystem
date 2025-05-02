using LibraryManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.DTOs.Author;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        // Get all authors
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var authors = await _authorRepository.GetAllAsync();
                var authorDtos = _mapper.Map<IEnumerable<AuthorDto>>(authors); // Mapping Author to AuthorDto
                return Ok(authorDtos);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Create a new author
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorCreateDTO authorCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  // Return validation errors
            }

            try
            {
                var author = _mapper.Map<Author>(authorCreateDTO);  // Map CreateDTO to Author
                var createdAuthor = await _authorRepository.AddAsync(author);
                var createdAuthorDto = _mapper.Map<AuthorDto>(createdAuthor);  // Map the created author to AuthorDto
                return CreatedAtAction(nameof(GetById), new { id = createdAuthorDto.Id }, createdAuthorDto);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get author by Id
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

                var authorDto = _mapper.Map<AuthorDto>(author); // Map Author to AuthorDto
                return Ok(authorDto);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Update an existing author
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

                _mapper.Map(authorUpdateDTO, existingAuthor); // Map AuthorUpdateDTO to existing Author
                var updatedAuthor = await _authorRepository.UpdateAsync(existingAuthor);
                var updatedAuthorDto = _mapper.Map<AuthorDto>(updatedAuthor); // Map the updated author to AuthorDto
                return Ok(updatedAuthorDto);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Delete an author
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
