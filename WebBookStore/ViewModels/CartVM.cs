using System.ComponentModel.DataAnnotations;
using WebBookStore.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebBookStore.ViewModels
{
    public class CartVM
    {
        public string BookCover { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string PublisherName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Amount { get; set; }
        public decimal ThanhTien => Amount * UnitPrice;

    }
}

