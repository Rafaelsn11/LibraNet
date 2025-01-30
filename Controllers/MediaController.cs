using LibraNet.Services.Interfaces;
using LibraNet.Models.Dtos.Media;
using Microsoft.AspNetCore.Mvc;
using LibraNet.Exceptions.ResponseError;
using LibraNet.Attributes;

namespace LibraNet.Controllers;

[AdminOnly]
public class MediaController : LibraNetBaseController
{
    private readonly IMediaService _service;
    public MediaController(IMediaService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MediaListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var media = await _service.GetMediaAsync();

        return Ok(media);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(MediaListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var media = await _service.GetMediaByIdAsync(id);

        return Ok(media);
    }

    [HttpPost]
    [ProducesResponseType(typeof(MediaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] MediaCreateDto media)
    {
        var mediaCreated = await _service.MediaCreateAsync(media);

        return Created(string.Empty, mediaCreated);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(MediaUpdateViewDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put([FromRoute] int id,
        [FromBody] MediaUpdateDto media)
    {
        var mediaUpdate = await _service.MediaUpdateAsync(id, media);

        return Ok(mediaUpdate);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _service.MediaDeleteAsync(id);

        return NoContent();
    }
}
