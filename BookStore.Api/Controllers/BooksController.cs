using System.Data;
using BookStore.Api.Models;
using BookStore.Api.Models.Dtos;
using BookStore.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController(BookService service) : ControllerBase
{
    // Get all books from database collection ....
    [HttpGet("GetAllBooksAsync", Name = "GetAllBooksAsync")]
    public async Task<Ok<List<BookDto>>> GetAll()
    {
        var books = await service.GetAllBooks();
        return TypedResults.Ok(books);
    }

    [HttpGet("GetBookByIdAsync/{id}", Name = "GetBookByIdAsync")]
    public async Task<Results<Ok<BookDto>, ProblemHttpResult>> GetById(Guid id)
    {
        var book = await service.GetById(id);
        if (book is null) return TypedResults.Problem($"Book with ID {id} not found.", statusCode: 404);

        return TypedResults.Ok(book);
    }

    [HttpPost("AddBookToStore", Name = "AddBookToStore")]
    public async Task<Results<CreatedAtRoute<BookDto>, ProblemHttpResult>> Create([FromBody] BookDto newBookDto)
    {
        if (string.IsNullOrWhiteSpace(newBookDto.Title))
        {
            return TypedResults.Problem(
                detail: "A book title cannot be empty.",
                statusCode: StatusCodes.Status400BadRequest);
        }

        try
        {
            var id = await service.AddBook(newBookDto);
            return TypedResults.CreatedAtRoute(newBookDto,
                "GetBookByIdAsync",
                new { id = newBookDto.Id});
        }
        catch (DuplicateNameException ex)
        {
            return TypedResults.Problem(
                detail: "A book with the same title exists in the collection.",
                statusCode: StatusCodes.Status409Conflict);
        }
        
    }

    [HttpPut("UpdateBookAsync", Name = "UpdateBookAsync") ]
    public async Task<Results<Ok<BookDto>, ProblemHttpResult>> Update(Guid id, BookDto bookDto)
    {
        if (string.IsNullOrWhiteSpace(bookDto.Title))
        {
            return TypedResults.Problem(
                detail: "Book Title cannot not be null.",
                statusCode: StatusCodes.Status400BadRequest);
        }

        await service.UpdateBook(bookDto);

        return TypedResults.Ok(bookDto);
    }


    [HttpDelete("DeleteBookBYiD{id}", Name = "DeleteBookById")]
    public async Task<Results<Ok<Guid>, ProblemHttpResult>> Delete(Guid id)
    {
        service.DeleteBook(id);
        
        return TypedResults.Ok(id);
    }
}


 