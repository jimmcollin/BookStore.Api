using System.Data;
using AutoMapper;
using BookStore.Api.DbContext;
using BookStore.Api.Models;
using BookStore.Api.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Services;

public class BookService(BookDbContext context, IMapper mapper)
{
    public async Task<List<BookDto>> GetAllBooks()
    {
        var books = await context.Books.ToListAsync();
        return books.Select(book => mapper.Map<BookDto>(book)).ToList();
    }

    public async Task<BookDto> GetById(Guid id)
    {
        var book = await context.Books.FirstOrDefaultAsync(book => book.Id == id);
        return mapper.Map<BookDto>(book);
    }

    public async Task<Guid> AddBook(BookDto bookDto)
    {
        var existingBook = await context.Books.FirstOrDefaultAsync(b => b.Title == bookDto.Title);
        if (existingBook != null)
            throw new DuplicateNameException($"Book with Title {bookDto.Title} all ready in collection.");
        var book = mapper.Map<Book>(bookDto);
        book.Id = Guid.NewGuid();
        
        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
        return book.Id;
    }

    public async Task<Guid> UpdateBook(BookDto bookDto)
    {
        var existingBook = await context.Books.FirstOrDefaultAsync(b => b.Id == bookDto.Id);
        if (existingBook == null) throw new KeyNotFoundException($"Book with ID {bookDto.Id} not found.");

        mapper.Map<Book>(bookDto);
        context.Books.Update(existingBook);
        await context.SaveChangesAsync();

        return bookDto.Id;
    }

    public async void DeleteBook(Guid id)
    {
        var book = await context.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (book == null) throw new KeyNotFoundException($"Book with ID {id} not found.");

        context.Books.Remove(book);
        await context.SaveChangesAsync();
    }
}
