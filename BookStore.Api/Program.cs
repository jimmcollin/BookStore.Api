using BookStore.Api.DbContext;
using BookStore.Api.Enums;
using BookStore.Api.Models;
using BookStore.Api.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace BookStore.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var db = new BookDbContext();

            // Create the database file and tables if they don't exist.
            db.Database.EnsureCreated();

            // Seed only when the Books table is empty.
            if (!db.Books.Any())
            {
                db.Books.AddRange(
                    new Book
                    {
                        Title = "Pride and Prejudice",
                        Author = "Jane Austen",
                        Isbn = "9780141439518",
                        PublishedYear = 1813,
                        CoverUrl = "",
                        OpenLibraryKeys = "",
                        Description = "A classic novel about love, class, and first impressions.",
                        Genre = Genre.Fiction,
                        Price = 12.99m,
                        Stock = 10
                    },
                    new Book
                    {
                        Title = "The Hobbit",
                        Author = "J. R. R. Tolkien",
                        Isbn = "9780547928227",
                        PublishedYear = 1937,
                        CoverUrl = "",
                        OpenLibraryKeys = "",
                        Description = "Bilbo Baggins joins a company of dwarves on an adventure.",
                        Genre = Genre.Fantasy,
                        Price = 15.99m,
                        Stock = 8
                    }
                );

                db.SaveChanges();
            }

            Console.WriteLine($"Database: {db.Database.GetDbConnection().DataSource}");
            Console.WriteLine($"Books: {db.Books.Count()}");

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<BookDbContext>();
            builder.Services.AddScoped<BookService>();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
