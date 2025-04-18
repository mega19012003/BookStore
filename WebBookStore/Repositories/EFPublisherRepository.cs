using Microsoft.EntityFrameworkCore;
using WebBookStore.Models;

namespace WebBookStore.Repositories
{
    public class EFPublisherRepository : IPublisherRepository
    {
        private readonly BookDbContext _context;
        public EFPublisherRepository(BookDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Publisher>> GetAllPublishersAsync()
        {
            return await _context.Publishers.ToListAsync();
        }
        public async Task<Publisher> GetPublisherByIdAsync(int id)
        {
            return await _context.Publishers.FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddPublisherAsync(Publisher publisher)
        {
            await _context.Publishers.AddAsync(publisher);
            await _context.SaveChangesAsync();
        }
        public async Task UpdatePublisherAsync(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
            await _context.SaveChangesAsync();
        }
        public async Task DeletePublisherAsync(int id)
        {
            var publisher = await GetPublisherByIdAsync(id);
            if (publisher != null)
            {
                publisher.IsDeleted = true; // isDeleted = true
                _context.Publishers.Update(publisher);
                await _context.SaveChangesAsync();
            }
        }
    }
}
