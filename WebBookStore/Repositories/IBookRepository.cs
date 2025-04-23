using WebBookStore.Models;

namespace WebBookStore.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<List<Book>> GetNewestBooksAsync(int count);
        Task<List<Book>> GetTopSellingBooksAsync(int count);
        Task<List<Book>> GetRandomBooksAsync(int count);
        Task<List<Book>> SearchBooksAsync(string keyword);
        Task<IEnumerable<Book>> GetAvailableBooksAsync();
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId);
        Task<IEnumerable<Book>> GetBooksByPublisherAsync(int authorId);
        Task<IEnumerable<Book>> GetBooksByCategoryAsync(int authorId);
        Task<Book> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}
