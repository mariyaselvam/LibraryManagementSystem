using AutoMapper;
using LibraryManagementSystem.DTOs.Genre;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenresController(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _genreRepository.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<GenreDto>>(genres);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null) return NotFound();

            var dto = _mapper.Map<GenreDto>(genre);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GenreCreateDto genreDto)
        {
            var genre = _mapper.Map<Genre>(genreDto);
            var created = await _genreRepository.AddAsync(genre);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<GenreDto>(created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GenreUpdateDto genreDto)
        {
            var existing = await _genreRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(genreDto, existing);
            var updated = await _genreRepository.UpdateAsync(existing);
            return Ok(_mapper.Map<GenreDto>(updated));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _genreRepository.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
