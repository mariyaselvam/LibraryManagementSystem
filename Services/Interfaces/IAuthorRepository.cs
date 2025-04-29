// Services/Interfaces/IAuthorService.cs
using LibraryManagementSystem.DTOs.Author;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDTO>> GetAllAuthorsAsync();
        Task<AuthorDTO?> GetAuthorByIdAsync(int id);
        Task<AuthorDTO> CreateAuthorAsync(AuthorCreateDTO authorCreateDTO);
        Task<AuthorDTO?> UpdateAuthorAsync(int id, AuthorUpdateDTO authorUpdateDTO);
        Task<bool> DeleteAuthorAsync(int id);
    }
}