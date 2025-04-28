using Microsoft.EntityFrameworkCore;
using WebBookStore.Models;

namespace WebBookStore.Repositories
{
    public interface IBillRepository
    {
        Task<IEnumerable<Bill>> GetAllBillsAsync();
        Task<Bill> GetBillByIdAsync(int id);
        Task DeleteBillAsync(Bill bill);
        Task UpdateBillAsync(Bill bill);
    }
}
