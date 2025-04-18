using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WebBookStore.Attributes;
using WebBookStore.Models;

namespace WebBookStore.ViewModels
{
    public class BookVM
    {
            public string ProductCode { get; set; } //Mã sp cho người dùng xem
            public string AuthorName { get; set; }  
            public string CategoryName { get; set; }
            public string PublisherName { get; set; }
            public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

            ////////////////////////////////////////////////////////////////////
            public string Title { get; set; }  
            public decimal Price { get; set; }
            public decimal? DiscountPrice { get; set; }
            public int PublishYear { get; set; }
            //public string? Translator { get; set; }  
            public string? Description { get; set; } 

            ////////////////////////////////////////////////////////////////////
            public string CoverUrl { get; set; }
            //public ICollection<ImageVM> Images { get; set; }

    }
}
