using LibraNet.Attributes;
using LibraNet.Exceptions.ResponseError;
using LibraNet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraNet.Controllers;

[AdminOnly]
public class AdminController : LibraNetBaseController
{
    private readonly IAdminService _service;
    public AdminController(IAdminService service)
    {
        _service = service;
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAllNotActive()
    {
        await _service.DeleteAllUserNotActive();

        return NoContent();
    }
}
