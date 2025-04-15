using WebBookStore.Models;

namespace WebBookStore.Repositories
{
    public interface IWishlistRepository
    {
        Task<IEnumerable<Wishlist>> GetAllWishlistsAsync();
        Task<Wishlist> GetWishlistByIdAsync(int id);
        Task AddWishlistAsync(Wishlist wishlist);
        Task UpdateWishlistAsync(Wishlist wishlist);
        Task DeleteWishlistAsync(int id);
    }
}
