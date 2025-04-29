using Microsoft.EntityFrameworkCore;
using WebBookStore.Models;

namespace WebBookStore.Repositories
{
    public class EFReviewRepository : IReviewRepository
    {
        private readonly BookDbContext _context;
        public EFReviewRepository(BookDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }
        public async Task<Review> GetReviewByIdAsync(int id)
        {
            return await _context.Reviews.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<Review>> GetReviewsByBookIdAsync(int bookId)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Where(r => r.BookId == bookId)
                .ToListAsync();
        }
        public async Task AddReviewAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteReviewAsync(int id)
        {
            var review = await GetReviewByIdAsync(id);
            if (review != null)
            {
                review.IsDeleted = true; // isDeleted = true
                _context.Reviews.Update(review);
                await _context.SaveChangesAsync();
            }
        }
    }
}
