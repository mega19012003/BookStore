using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebBookStore.Models
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Cart> Cats { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
