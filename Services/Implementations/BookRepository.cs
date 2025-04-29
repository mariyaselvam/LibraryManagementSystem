using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services.Implementations
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        // Fetch a book by ID along with its Author
        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        }

        // Fetch all books
        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.Include(b => b.Author).ToListAsync();
        }

        // Add a new book to the database
        public async Task<Book> AddAsync(Book book)
        {
            _context.Books.Add(book); 
            await _context.SaveChangesAsync(); 
            return book;
        }

        // Update an existing book
        public async Task<Book> UpdateAsync(Book book)
        {
            _context.Books.Update(book); 
            await _context.SaveChangesAsync();
            return book;
        }

        // Delete a book from the database
        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
