using LibraNet.Exceptions.ExceptionsBase;
using LibraNet.Models.Dtos.Edition;
using LibraNet.Models.Dtos.User;
using LibraNet.Repository.Interfaces;
using LibraNet.Services.Interfaces;

namespace LibraNet.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<UserDetailDto> GetUserByIdAsync(Guid id)
    {
        var user = await _repository.GetUserByIdAsync(id);

        if (user == null)
            throw new NotFoundException("User not found");

        var userDetail = new UserDetailDto(
            user.Name, user.BirthDate, user.IsActive,
            user.Loans.Select(x => new EditionDto(x.Id, x.Year, x.Status)).ToList()
        );

        return userDetail;
    }

    public async Task<IEnumerable<UserListDto>> GetUsersAsync()
    {
        var user = await _repository.GetUsersAsync();

        var userLists = user.Select(x => new UserListDto(x.Id, x.Name, x.IsActive));

        return userLists;
    }
}
