using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author> GetByIdAsync(int id);
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author> AddAsync(Author author);
        Task<Author> UpdateAsync(Author author);
        Task<bool> DeleteAsync(int id);
    }
}

