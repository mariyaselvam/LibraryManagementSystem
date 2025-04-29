// Services/Implementations/AuthorService.cs
using AutoMapper;
using LibraryManagementSystem.DTOs.Author;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Interfaces;
using LibraryManagementSystem.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorDTO>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorDTO>>(authors);
        }

        public async Task<AuthorDTO?> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            return _mapper.Map<AuthorDTO?>(author);
        }

        public async Task<AuthorDTO> CreateAuthorAsync(AuthorCreateDTO authorCreateDTO)
        {
            var author = _mapper.Map<Author>(authorCreateDTO);
            var createdAuthor = await _authorRepository.AddAsync(author);
            return _mapper.Map<AuthorDTO>(createdAuthor);
        }

        public async Task<AuthorDTO?> UpdateAuthorAsync(int id, AuthorUpdateDTO authorUpdateDTO)
        {
            var existingAuthor = await _authorRepository.GetByIdAsync(id);
            if (existingAuthor == null)
            {
                return null;
            }
            _mapper.Map(authorUpdateDTO, existingAuthor);
            var updatedAuthor = await _authorRepository.UpdateAsync(existingAuthor);
            return _mapper.Map<AuthorDTO?>(updatedAuthor);
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            return await _authorRepository.DeleteAsync(id);
        }
    }
}