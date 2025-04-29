// Services/Interfaces/IBookService.cs
using LibraryManagementSystem.DTOs.Book;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookResponseDTO>> GetAllBooksAsync();
        Task<BookResponseDTO?> GetBookByIdAsync(int id);
        Task<BookResponseDTO> CreateBookAsync(BookCreateDTO bookCreateDTO);
        Task<BookResponseDTO?> UpdateBookAsync(int id, BookUpdateDTO bookUpdateDTO);
        Task<bool> DeleteBookAsync(int id);
    }
}