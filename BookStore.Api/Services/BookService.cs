using BookStore.Api.DbContext;
using BookStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Services
{
    public class BookService(BookDbContext _context)
    {
        public async Task<List<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }
    }
}
