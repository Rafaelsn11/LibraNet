using LibraNet.Models.Dtos.User;

namespace LibraNet.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserListDto>> GetUsersAsync();
    Task<UserDetailDto> GetUserByIdAsync(Guid id);
    Task<UserDto> UserCreateAsync(UserCreateDto user);
    Task<UserProfileDto> GetProfileAsync();
    Task UserUpdateAsync(UserUpdateDto userUpdate);
    Task UserChangePasswordAsync(UserChangePassword userChangePassword);
    Task UserAccountOff();
}
