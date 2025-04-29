using WebBookStore.Models;

namespace WebBookStore.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(int id);
        Task<IEnumerable<Review>> GetReviewsByBookIdAsync(int bookId);
        Task AddReviewAsync(Review review);
        Task DeleteReviewAsync(int id);
    }
}
