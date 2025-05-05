

using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services.Interfaces
{
    public interface IAuthorRepository
    {
        Task<Author?> GetByIdAsync(int id, bool includeDeleted = false);
        Task<IEnumerable<Author>> GetAllAsync(bool includeDeleted = false);
        Task<Author> AddAsync(Author author);
        Task<Author> UpdateAsync(Author author);
        Task<bool> DeleteAsync(int id); // You already have this
    }
}

