using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WebBookStore.Attributes;
using WebBookStore.Models;

namespace WebBookStore.ViewModels
{
    public class BookDetailVM
    {
        public int Id { get; set; }
        public string ProductCode { get; set; } //Mã sp cho người dùng xem

        public int AuthorId { get; set; }
        public string AuthorName { get; set; } //Tên tác giả

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public int PublisherId { get; set; }
        public string PublisherName { get; set; }

        ////////////////////////////////////////////////////////////////////
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int PublishYear { get; set; }
        public int Quantity { get; set; } //Số lượng tồn kho
        public string? Description { get; set; }
        public bool IsDeleted { get; set; } = false; //Soft Delete
        public bool OutOfStock { get; set; } = false;

        ////////////////////////////////////////////////////////////////////
        public string CoverUrl { get; set; }
        //public ICollection<Image>? Images { get; set; } //= new List<Image>();

        [NotMapped]
        public decimal FinalPrice
        => DiscountPrice.HasValue
        ? Price * (100 - DiscountPrice.Value) / 100
        : Price;

        public List<ReviewVM> Reviews { get; set; }

        public double AverageRating { get; set; }
    }
}
