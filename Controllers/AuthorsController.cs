using LibraryManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.DTOs.Author;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

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

        // ? Publicly accessible - any authenticated user
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var authors = await _authorRepository.GetAllAsync();
                var authorDtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);
                return Ok(authorDtos);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // ? Only Admins can create authors
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorCreateDTO authorCreateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var author = _mapper.Map<Author>(authorCreateDTO);
                var createdAuthor = await _authorRepository.AddAsync(author);
                var createdAuthorDto = _mapper.Map<AuthorDto>(createdAuthor);
                return CreatedAtAction(nameof(GetById), new { id = createdAuthorDto.Id }, createdAuthorDto);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // ? Accessible to all authenticated users
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var author = await _authorRepository.GetByIdAsync(id);
                if (author == null)
                    return NotFound();

                var authorDto = _mapper.Map<AuthorDto>(author);
                return Ok(authorDto);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // ? Only Admins can update authors
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorUpdateDTO authorUpdateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingAuthor = await _authorRepository.GetByIdAsync(id);
                if (existingAuthor == null)
                    return NotFound();

                _mapper.Map(authorUpdateDTO, existingAuthor);
                var updatedAuthor = await _authorRepository.UpdateAsync(existingAuthor);
                var updatedAuthorDto = _mapper.Map<AuthorDto>(updatedAuthor);
                return Ok(updatedAuthorDto);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // ? Only Admins can delete authors
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var author = await _authorRepository.GetByIdAsync(id);
                if (author == null)
                    return NotFound();

                var isDeleted = await _authorRepository.DeleteAsync(id);
                if (isDeleted)
                    return NoContent();
                else
                    return BadRequest("Failed to delete the author.");
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
