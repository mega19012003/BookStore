using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebBookStore.Models
{
    public class BookDbContext : IdentityDbContext<AppUser>
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        //public DbSet<Cart> Carts { get; set; }
        //public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        // public DbSet<Image> Images { get; set; }
        // public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<AppUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Book>()
                .HasOne(p => p.Category)
                .WithMany(p => p.Books)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Book>()
                .HasOne(p => p.Author)
                .WithMany(p => p.Books)
                .HasForeignKey(p => p.AuthorId);

            modelBuilder.Entity<Book>()
                .HasOne(p => p.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(p => p.PublisherId);

            modelBuilder.Entity<Image>()
                .HasOne(i => i.Book)
                .WithMany(b => b.Images)
                .HasForeignKey(i => i.BookId);*/
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Review>()
            .HasOne(r => r.Book)
            .WithMany(b => b.Reviews)
            .HasForeignKey(r => r.BookId);  
            //modelBuilder.Entity<CartItem>().HasNoKey(); // 👈 KHÔNG có khóa chính
        }

    }
}
