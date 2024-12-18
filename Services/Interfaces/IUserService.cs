using LibraNet.Models.Dtos.User;

namespace LibraNet.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserListDto>> GetUsersAsync();
    Task<UserDetailDto> GetUserByIdAsync(Guid id);
}
