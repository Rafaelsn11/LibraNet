using LibraNet.Exceptions.ExceptionsBase;
using LibraNet.Models.Dtos.Token;
using LibraNet.Models.Dtos.User;
using LibraNet.Repository.Interfaces;
using LibraNet.Security.Tokens.Interfaces;
using LibraNet.Services.Cryptography;
using LibraNet.Services.Interfaces;

namespace LibraNet.Services;

public class LoginService : ILoginService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _acessTokenGenerator;

    public LoginService(
        IUserRepository repository, 
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator acessTokenGenerator)
    {
        _repository = repository;
        _passwordEncripter = passwordEncripter;
        _acessTokenGenerator = acessTokenGenerator;
    }

    public async Task<UserDto> ExecuteLogin(UserLoginDto userLogin)
    {
        var salt = await GetBySalt(userLogin.Email);
        var saltUser = salt.ToString();

        var encriptedPassword = _passwordEncripter.Encrypt(userLogin.Password, saltUser!);
        var user = await _repository.GetByEmailAndPassword(userLogin.Email, encriptedPassword);

        if(user == null)
            throw new InvalidLoginException();

        var tokens = new TokenDto(await _acessTokenGenerator.Generate(user.UserIdentifier));
        return new UserDto
        (
            user.Name,
            tokens
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
