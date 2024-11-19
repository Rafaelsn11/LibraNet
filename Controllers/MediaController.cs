using LibraNet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraNet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediaController : ControllerBase
{
    private readonly IMediaService _service;
    public MediaController(IMediaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var media = await _service.GetMediaAsync();

        return Ok(media);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var media = await _service.GetMediaByIdAsync(id);

        return Ok(media);
    }
}
