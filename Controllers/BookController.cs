using LibraNet.Models.Dtos.Book;
using LibraNet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraNet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _service;
    public BookController(IBookService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var books = await _service.GetBooksAsync();

        return Ok(books);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var book = await _service.GetBookByIdAsync(id);
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BookCreateDto book)
    {
        var bookCreated = await _service.BookCreateAsync(book);

        return Created(string.Empty, bookCreated);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id,
        [FromBody] BookUpdateDto book)
    {
        var bookUpdate = await _service.BookUpdateAsync(id, book);

        return Ok(bookUpdate);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _service.BookDeleteAsync(id);

        return NoContent();
    }
}
