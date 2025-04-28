using WebBookStore.Models;

namespace WebBookStore.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(int id);
        Task AddReviewAsync(Review review);
        Task DeleteReviewAsync(int id);
    }
}
