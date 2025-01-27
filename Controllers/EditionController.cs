using LibraNet.Attributes;
using LibraNet.Exceptions.ResponseError;
using LibraNet.Models.Dtos.Edition;
using LibraNet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraNet.Controllers;

public class EditionController : LibraNetBaseController
{
    private readonly IEditionService _service;
    public EditionController(IEditionService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EditionListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var editions = await _service.GetEditionsAsync();

        return Ok(editions);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(EditionDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var edition = await _service.GetEditionByIdAsync(id);

        return Ok(edition);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EditionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] EditionCreateDto edition)
    {
        var editionCreated = await _service.EditionCreateAsync(edition);

        return Created(string.Empty, editionCreated);
    }

    [HttpPut]
    [Route("loan/{editionId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status409Conflict)]
    [AuthenticatedUser]
    public async Task<IActionResult> Loan([FromRoute] int editionId)
    {
        await _service.EditionLoanAsync(editionId, DateTime.UtcNow);

        return NoContent();
    }

    [HttpPut]
    [Route("return/{editionId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status409Conflict)]
    [AuthenticatedUser]
    public async Task<IActionResult> Return([FromRoute] int editionId)
    {
        await _service.EditionReturnAsync(editionId);

        return NoContent();
    }
}
