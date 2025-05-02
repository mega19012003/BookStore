using Microsoft.AspNetCore.Mvc;
using WebBookStore.Repositories;
using WebBookStore.ViewModels;

namespace WebBookStore.ViewComponents
{
    public class FeaturedBooksViewComponent : ViewComponent
    {
        private readonly IBookRepository _bookRepository;
        private readonly IReviewRepository _reviewRepository;

        public FeaturedBooksViewComponent(IBookRepository bookRepository, IReviewRepository reviewRepository)
        {
            _bookRepository = bookRepository;
            _reviewRepository = reviewRepository;
        }

        // Phương thức lấy sách bán chạy
        public async Task<IViewComponentResult> InvokeAsync(int authorId, int currentBookId, int count)
        {
            var featuredBooks = await _bookRepository.GetBooksByAuthorAsync(authorId);
            var randomFeatured = featuredBooks
                .Where(b => b.Id != currentBookId)
                .OrderBy(_ => Guid.NewGuid())
                .Take(count)
                .ToList();
            var booksWithRatings = new List<FeaturedBookVM>();

            foreach (var book in randomFeatured)
            {
                var reviews = await _reviewRepository.GetReviewsByBookIdAsync(book.Id);
                double averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;

                booksWithRatings.Add(new FeaturedBookVM
                {
                    Id = book.Id,
                    Title = book.Title,
                    Price = book.Price,
                    DiscountPrice = book.DiscountPrice,
                    CoverUrl = book.CoverUrl,
                    AverageRating = averageRating,
                    Quantity = book.Quantity
                });
            }
            return View(booksWithRatings);
        }
    }
}
