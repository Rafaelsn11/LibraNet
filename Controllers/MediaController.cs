using LibraNet.Services.Interfaces;
using LibraNet.Models.Dtos.Media;
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

    [HttpPost]
    public async Task<IActionResult> Post(MediaCreateDto media)
    {
        var mediaCreated = await _service.MediaCreateAsync(media);

        return Created(string.Empty, mediaCreated);
    }
}
