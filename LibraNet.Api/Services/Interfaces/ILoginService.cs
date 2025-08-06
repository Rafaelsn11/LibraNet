using LibraNet.Models.Dtos.User;

namespace LibraNet.Services.Interfaces;

public interface ILoginService
{
    Task<UserDto> ExecuteLogin(UserLoginDto userLogin);
}
