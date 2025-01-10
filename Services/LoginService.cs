using LibraNet.Exceptions.ExceptionsBase;
using LibraNet.Models.Dtos.User;
using LibraNet.Repository.Interfaces;
using LibraNet.Services.Cryptography;
using LibraNet.Services.Interfaces;

namespace LibraNet.Services;

public class LoginService : ILoginService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordEncripter _passwordEncripter;

    public LoginService(IUserRepository repository, IPasswordEncripter passwordEncripter)
    {
        _repository = repository;
        _passwordEncripter = passwordEncripter;
    }

    public async Task<UserDto> ExecuteLogin(UserLoginDto userLogin)
    {
        var salt = await GetBySalt(userLogin.Email);
        var saltUser = salt.ToString();

        var encriptedPassword = _passwordEncripter.Encrypt(userLogin.Password, saltUser!);
        var user = await _repository.GetByEmailAndPassword(userLogin.Email, encriptedPassword);

        if(user == null)
            throw new InvalidLoginException();

        return new UserDto
        (
            user.Name
        );
    }

    private async Task<string> GetBySalt(string email)
    {
        var user = await _repository.GetUserByEmail(email);

        if(user == null)
            throw new NotFoundException("User not found");

        return user.Salt;
    }
}
