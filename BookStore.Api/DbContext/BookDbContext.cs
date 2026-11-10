using BookStore.Api.DbContext;
using BookStore.Api.Models;
using Microsoft.EntityFrameworkCore;

//using var db = new BookDbContext();
//db.Database.EnsureCreated();

namespace BookStore.Api.DbContext
{
    public class BookDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Book> Books => Set<Book>();

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("Data Source=books.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var book = modelBuilder.Entity<Book>();

            book.HasKey(b => b.Id);

            book.Property(b => b.Title).IsRequired().HasMaxLength(255);
            book.Property(b => b.Author).IsRequired().HasMaxLength(255);
            book.Property(b => b.Isbn).IsRequired().HasMaxLength(40);
            book.Property(b => b.CoverUrl).IsRequired().HasMaxLength(255);
            book.Property(b => b.OpenLibraryKeys).IsRequired().HasMaxLength(255);
            book.Property(b => b.Description).IsRequired().HasMaxLength(1024);

            book.Property(b => b.Genre).HasConversion<int>();
        }
    }
}
