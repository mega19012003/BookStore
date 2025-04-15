using WebBookStore.Models;

namespace WebBookStore.Repositories
{
    public interface IImageRepository
    {
        Task<IEnumerable<Image>> GetAllImagesAsync();
        Task<Image> GetImageByIdAsync(int id);
        Task AddImageAsync(Image image);
        Task UpdateImageAsync(Image image);
        Task DeleteImageAsync(int id);
    }
}
