using LibraNet.Attributes;
using LibraNet.Models.Dtos.User;
using LibraNet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraNet.Controllers;

public class UserController : LibraNetBaseController
{
    private readonly IUserService _service;
    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await _service.GetUsersAsync();

        return Ok(users);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var user = await _service.GetUserByIdAsync(id);

        return Ok(user);
    }

    [HttpGet]
    [Route("/profile")]
    [AuthenticatedUser]
    public async Task<IActionResult> GetUserProfile()
    {
        var profile = await _service.GetProfileAsync();

        return Ok(profile);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UserCreateDto user)
    {
        var userCreated = await _service.UserCreateAsync(user);

        return Created(string.Empty, userCreated);
    }
}
