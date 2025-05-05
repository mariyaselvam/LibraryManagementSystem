using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services.Interfaces
{

    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(int id);
        Task<IEnumerable<Book>> GetAllAsync(bool includeDeleted = false);
        Task<Book> AddAsync(Book book);
        Task<Book> UpdateAsync(Book book);
        Task<bool> DeleteAsync(int id);
    }
}

