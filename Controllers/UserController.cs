using LibraNet.Attributes;
using LibraNet.Exceptions.ResponseError;
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
    [ProducesResponseType(typeof(IEnumerable<UserListDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get()
    {
        var users = await _service.GetUsersAsync();

        return Ok(users);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(UserDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]    
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var user = await _service.GetUserByIdAsync(id);

        return Ok(user);
    }

    [HttpGet("/profile")]
    [ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]   
    [AuthenticatedUser]
    public async Task<IActionResult> GetUserProfile()
    {
        var profile = await _service.GetProfileAsync();

        return Ok(profile);
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorsJson),StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] UserCreateDto user)
    {
        var userCreated = await _service.UserCreateAsync(user);

        return Created(string.Empty, userCreated);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson),StatusCodes.Status400BadRequest)]
    [AuthenticatedUser]
    public async Task<IActionResult> Update([FromBody] UserUpdateDto userUpdate)
    {
        await _service.UserUpdateAsync(userUpdate);

        return NoContent();
    }

    [HttpPut("change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorsJson),StatusCodes.Status400BadRequest)]
    [AuthenticatedUser]
    public async Task<IActionResult> ChangePassword(UserChangePassword userChangePassword)
    {
        await _service.UserChangePasswordAsync(userChangePassword);
        
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [AuthenticatedUser]
    public async Task<IActionResult> AccountOff()
    {
        await _service.UserAccountOff();

        return NoContent();
    }
}
