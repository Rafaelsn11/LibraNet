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
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLogin)
    {
        var user = await _service.ExecuteLogin(userLogin);

        return Ok(user);
    }
}
