using LibraNet.Attributes;
using LibraNet.Exceptions.ResponseError;
using LibraNet.Models.Dtos.Book;
using LibraNet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraNet.Controllers;

[AdminOnly]
public class BookController : LibraNetBaseController
{
    private readonly IBookService _service;
    public BookController(IBookService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var books = await _service.GetBooksAsync();

        return Ok(books);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(BookDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var book = await _service.GetBookByIdAsync(id);
        return Ok(book);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] BookCreateDto book)
    {
        var bookCreated = await _service.BookCreateAsync(book);

        return Created(string.Empty, bookCreated);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(BookUpdateViewDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put([FromRoute] int id,
        [FromBody] BookUpdateDto book)
    {
        var bookUpdate = await _service.BookUpdateAsync(id, book);

        return Ok(bookUpdate);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _service.BookDeleteAsync(id);

        return NoContent();
    }
}
