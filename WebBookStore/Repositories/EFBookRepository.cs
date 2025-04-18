using Microsoft.EntityFrameworkCore;
using WebBookStore.Models;

namespace WebBookStore.Repositories
{
    public class EFBookRepository : IBookRepository
    {
        private readonly BookDbContext _context;
        public EFBookRepository(BookDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(p => p.Category)
                .Include(p => p.Author)
                .Include(p => p.Publisher)
                .ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
        {
            return await _context.Books
                .Where(p => p.IsDeleted == false && p.OutOfStock == false) // isDeleted = false
                .Include(p => p.Category)
                .Include(p => p.Author)
                .Include(p => p.Publisher)
                .ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId)
        {
            return await _context.Books
                .Where(p => p.AuthorId == authorId && !p.IsDeleted)
                .Where(p => p.IsDeleted == false && p.OutOfStock == false)
                .Include(p => p.Category)
                .Include(p => p.Author)
                .Include(p => p.Publisher)
                .ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetBooksByPublisherAsync(int publisherId)
        {
            return await _context.Books
                .Where(p => p.PublisherId == publisherId && !p.IsDeleted)
                .Where(p => p.IsDeleted == false && p.OutOfStock == false)
                .Include(p => p.Category)
                .Include(p => p.Author)
                .Include(p => p.Publisher)
                .ToListAsync();
        }
        public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(int categoryId)
        {
            return await _context.Books
                .Where(p => p.CategoryId == categoryId && !p.IsDeleted)
                .Where(p => p.IsDeleted == false && p.OutOfStock == false)
                .Include(p => p.Category)
                .Include(p => p.Author)
                .Include(p => p.Publisher)
                .ToListAsync();
        }
        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books
                .Include(p => p.Category)
                .Include(p => p.Author)
                .Include(p => p.Publisher)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddBookAsync(Book book)
        {
            try
            {
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log lỗi chi tiết vào file log
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteBookAsync(int id)
        {
            var book = await GetBookByIdAsync(id);
            if (book != null)
            {
                book.IsDeleted = true; // isDeleted = true
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
            }
        }
    }
}
