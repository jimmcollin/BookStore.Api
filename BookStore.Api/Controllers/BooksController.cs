using BookStore.Api.Models;
using BookStore.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController(BookService service) : ControllerBase
{
    [HttpGet("GetAllBooksAsync", Name = "GetAllBooksAsync")]
    public async Task<Results<Ok<List<Book>>, ProblemHttpResult>> GetAll()
    {
        var books = await service.GetAllBooks();
        return TypedResults.Ok(books);
    }

    [HttpGet("GetBookByIdAsync{id}", Name = "GetBookByIdAsync")]
    public async Task<Results<Ok<Book>, ProblemHttpResult>> GetById(Guid id)
    {
        
    }
}

