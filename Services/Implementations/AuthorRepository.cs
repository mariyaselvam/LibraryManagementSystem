using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Services.Implementations
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Author?> GetByIdAsync(int id, bool includeDeleted = false)
        {
            return await _context.Authors
                .Where(a => a.Id == id && (includeDeleted || !a.IsDeleted))
                .FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<Author>> GetAllAsync(bool includeDeleted = false)
        {
            return await _context.Authors
                .Where(a => includeDeleted || !a.IsDeleted)
                .ToListAsync();
        }


        public async Task<Author> AddAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null || author.IsDeleted)
                return false;

            author.IsDeleted = true;
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
