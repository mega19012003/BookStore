﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebBookStore.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public AppUser User { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Range(1, 5, ErrorMessage = "Đánh giá phải từ 1 đến 5 sao.")]
        public int Rating { get; set; }

        [StringLength(1000)]
        public string? Comment { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false; //Soft Delete

    }
}

