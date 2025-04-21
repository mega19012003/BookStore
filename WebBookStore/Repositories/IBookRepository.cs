using WebBookStore.Models;

namespace WebBookStore.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<List<Book>> GetNewestBooksAsync(int count);
        Task<List<Book>> GetNewestBooksByCategoryAsync(int id, int count);
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
