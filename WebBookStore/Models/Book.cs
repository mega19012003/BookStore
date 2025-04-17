﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WebBookStore.Attributes;

namespace WebBookStore.Models
{
    public class Book
    {
            [Key]
            [Required]
            public int Id { get; set; }  
            public string ProductCode { get; set; } //Mã sp cho người dùng xem
            [DisplayName("Tác giả")]

            public int AuthorId { get; set; }  
            public Author Author { get; set; } 
            [DisplayName("Thể loại")]

            public int CategoryId { get; set; }
            public Category Category { get; set; }

            [DisplayName("Nhà xuất bản")]
            public int PublisherId { get; set; }
            public Publisher Publisher { get; set; }

            [DisplayName("Đánh giá")]
            public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

            ////////////////////////////////////////////////////////////////////
            [Required]
            [DisplayName("Tiêu đề")]
            [StringLength(100, ErrorMessage = "Tiêu đề không được vượt quá 100 ký tự.")]
            public string Title { get; set; }  
            [Required]
            [Range(10000, 500000000, ErrorMessage = "Giá tiền phải nằm trong khoảng 10.000 - 50.000.000")]
            [DisplayName("Giá tiền")]
            public decimal Price { get; set; }
            [Range(5, 80, ErrorMessage = "% số tiền giảm phải nằm từ 5% - 80%")]
            [DisplayName("Giảm giá")]
            public decimal? DiscountPrice { get; set; }
            [DisplayName("Năm phát hành")]
            [ValidYearAttribute(ErrorMessage = "Năm xuất bản không hợp lệ. Năm phải từ khoảng 1300 - hiện tại")]
            public int PublishYear { get; set; }
            //public string? Translator { get; set; }  
            [DisplayName("Mô tả")]
            [DataType(DataType.MultilineText)]
            [StringLength(2000, ErrorMessage = "Mô tả không được vượt quá 2000 ký tự.")]
            public string? Description { get; set; } 
            public bool IsDeleted { get; set; } = false; //Soft Delete
            public bool OutOfStock { get; set; } = false;

            ////////////////////////////////////////////////////////////////////
            [Required]
            [DisplayName("Hình ảnh")]
            [StringLength(1024)]
            public string CoverUrl { get; set; }
            public ICollection<Image> Images { get; set; }

    }
}
