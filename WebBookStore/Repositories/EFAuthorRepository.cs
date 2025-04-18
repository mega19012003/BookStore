using Microsoft.EntityFrameworkCore;
using WebBookStore.Models;

namespace WebBookStore.Repositories
{
    public class EFAuthorRepository : IAuthorRepository
    {
        private readonly BookDbContext _context;
        public EFAuthorRepository(BookDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }
        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAuthorAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAuthorAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAuthorAsync(int id)
        {
            var author = await GetAuthorByIdAsync(id);
            if (author != null)
            {
                author.IsDeleted = true; // isDeleted = true
                _context.Authors.Update(author);
                await _context.SaveChangesAsync();
            }
        }
    }
}
