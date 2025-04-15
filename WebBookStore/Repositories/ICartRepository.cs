using WebBookStore.Models;

namespace WebBookStore.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetAllCartsAsync();
        Task<Cart> GetCartByIdAsync(int id);
        Task AddCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(int id);
    }
}
