using WebBookStore.Models;

namespace WebBookStore.Repositories
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<Publisher>> GetAllPublishersAsync();
        Task<Publisher> GetPublisherByIdAsync(int id);
        Task AddPublisherAsync(Publisher publisher);
        Task UpdatePublisherAsync(Publisher publisher);
        Task DeletePublisherAsync(int id);
    }
}
