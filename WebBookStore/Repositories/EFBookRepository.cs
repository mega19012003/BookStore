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
                .Where(r => !r.IsDeleted)
                .Include(p => p.Category)
                .Include(p => p.Author)
                .Include(p => p.Publisher)
                .ToListAsync();
        }
        public async Task<List<Book>> GetNewestBooksAsync(int count)
        {
            return await _context.Books
                .Where(r => !r.IsDeleted)
                .Include(p => p.Category)
                .Include(p => p.Author)
                .Include(p => p.Publisher)
                .OrderByDescending(b => b.Id)
                .Take(count)
                .ToListAsync();
        }
        public async Task<List<Book>> GetTopSellingBooksAsync(int count)
        {
            return await _context.Books
                .Where(r => !r.IsDeleted)
                .OrderByDescending(b => b.soldQuantity)
                .Take(count)
                .Include(b => b.Category) // nếu cần hiển thị category
                .ToListAsync();
        }
        public async Task<List<Book>> GetRandomBooksAsync(int count)
        {
            var allBooks = await _context.Books.Include(p=>p.Category).Where(r => !r.IsDeleted).ToListAsync();
            return allBooks.OrderBy(b => Guid.NewGuid())  // Sắp xếp ngẫu nhiên
                           .Take(count)                  // Lấy 8 sách
                           .ToList();
        }

        public async Task<List<Book>> SearchBooksAsync(string keyword)
        {
            return await _context.Books
                .Where(b => b.Title.Contains(keyword))
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
        {
            return await _context.Books
                .Where(p => p.IsDeleted == false /*&& p.OutOfStock == false*/) // isDeleted = false
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
        public async Task<List<Book>> GetNewestBooksByCategoryAsync(int id, int count)
        {
            return await _context.Books
                .Include(p => p.Category)
                .Include(p => p.Author)
                .Include(p => p.Publisher)
                .Where(b => b.Category.Id == id)
                .Take(count)
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
