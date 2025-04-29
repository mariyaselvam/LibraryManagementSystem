// Services/Implementations/BookService.cs
using AutoMapper;
using LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Interfaces;
using LibraryManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookResponseDTO>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookResponseDTO>>(books);
        }

        public async Task<BookResponseDTO?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            return _mapper.Map<BookResponseDTO?>(book);
        }

        public async Task<BookResponseDTO> CreateBookAsync(BookCreateDTO bookCreateDTO)
        {
            var book = _mapper.Map<Book>(bookCreateDTO);
            var createdBook = await _bookRepository.AddAsync(book);
            var response = _mapper.Map<BookResponseDTO>(createdBook);
            var author = await _authorRepository.GetByIdAsync(createdBook.AuthorId);
            response.AuthorName = author?.Name;
            return response;
        }

        public async Task<BookResponseDTO?> UpdateBookAsync(int id, BookUpdateDTO bookUpdateDTO)
        {
            var existingBook = await _bookRepository.GetByIdAsync(id);
            if (existingBook == null)
            {
                return null;
            }
            _mapper.Map(bookUpdateDTO, existingBook);
            var updatedBook = await _bookRepository.UpdateAsync(existingBook);
            return _mapper.Map<BookResponseDTO?>(updatedBook);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            return await _bookRepository.DeleteAsync(id);
        }
    }
}