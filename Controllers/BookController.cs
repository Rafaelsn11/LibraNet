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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _service.GetBookByIdAsync(id);
        return Ok(book);
    }
}
