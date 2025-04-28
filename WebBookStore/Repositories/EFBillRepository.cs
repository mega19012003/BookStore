using Microsoft.EntityFrameworkCore;
using WebBookStore.Models;

namespace WebBookStore.Repositories
{
    public class EFBillRepository : IBillRepository
    {
        private readonly BookDbContext _context;
        public EFBillRepository(BookDbContext context)
        {
            _context = context;
        }

        public async Task<Bill> GetBillByIdAsync(int id)
        {
            return await _context.Bills.FindAsync(id);
        }

        public async Task<IEnumerable<Bill>> GetAllBillsAsync()
        {
            return await _context.Bills.ToListAsync();
        }

        public async Task DeleteBillAsync(Bill bill)
        {
            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBillAsync(Bill bill)
        {
            _context.Bills.Update(bill);
            await _context.SaveChangesAsync();
        }
    }
}
