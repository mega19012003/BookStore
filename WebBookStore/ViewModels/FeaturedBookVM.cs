using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using WebBookStore.Attributes;
using WebBookStore.Models;

namespace WebBookStore.ViewModels
{
    public class FeaturedBookVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int Quantity { get; set; }
        [NotMapped]
        public decimal FinalPrice
        => DiscountPrice.HasValue
        ? Price * (100 - DiscountPrice.Value) / 100
        : Price;
        public string CoverUrl { get; set; }
        public double AverageRating { get; set; }
    }
}
