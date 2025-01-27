using LibraNet.Exceptions.ResponseError;
using LibraNet.Models.Dtos.User;
using LibraNet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraNet.Controllers;

public class LoginController : LibraNetBaseController
{
    private readonly ILoginService _service;
    public LoginController(ILoginService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
    {
        var user = await _service.ExecuteLogin(userLogin);

        return Ok(user);
    }
}
