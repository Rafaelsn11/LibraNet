using LibraNet.Attributes;
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

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] EditionCreateDto edition)
    {
        var editionCreated = await _service.EditionCreateAsync(edition);

        return Created(string.Empty, editionCreated);
    }

    [HttpPut]
    [Route("loan/{editionId}")]
    [AuthenticatedUser]
    public async Task<IActionResult> Loan([FromRoute] int editionId)
    {
        await _service.EditionLoanAsync(editionId, DateTime.UtcNow);

        return NoContent();
    }

    [HttpPut]
    [Route("return/{editionId}")]
    [AuthenticatedUser]
    public async Task<IActionResult> Return([FromRoute] int editionId)
    {
        await _service.EditionReturnAsync(editionId);

        return NoContent();
    }
}
