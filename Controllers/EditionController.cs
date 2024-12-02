using LibraNet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraNet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EditionController : ControllerBase
{
    private readonly IEditionService _service;
    public EditionController(IEditionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var editions = await _service.GetEditionsAsync();

        return Ok(editions);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var edition = await _service.GetEditionByIdAsync(id);

        return Ok(edition);
    }
}
