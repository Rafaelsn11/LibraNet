using FluentAssertions;
using LibraNet.Exceptions.ExceptionsBase;
using LibraNet.Models.Dtos.User;
using LibraNet.Models.Entities;
using LibraNet.Services;
using LibraNet.Tests.CommonTestUtilities.Cryptography;
using LibraNet.Tests.CommonTestUtilities.Dtos;
using LibraNet.Tests.CommonTestUtilities.Models.Entities;
using LibraNet.Tests.CommonTestUtilities.Repository;
using LibraNet.Tests.CommonTestUtilities.Tokens;

namespace LibraNet.Tests.Services.Login;

public class LoginTest
{
    [Fact]
    public async Task Sucess()
    {
        (var user, var password) = UserBuild.Build();

        var service = CreateService(user);

        var result = await service.ExecuteLogin(new UserLoginDto(user.Email, password));

        result.Should().NotBeNull();
        result.Tokens.Should().NotBeNull();
        result.Name.Should().NotBeNullOrWhiteSpace().And.Be(user.Name);
        result.Tokens.AccessToken.Should().NotBeNullOrEmpty();
    }

    [Fact]

    public async Task Error_NotFound_User()
    {
        var request = UserLoginDtoBuilder.Build();

        var service = CreateService();

        Func<Task> act = async () => { await service.ExecuteLogin(request); };

        await act.Should().ThrowAsync<NotFoundException>()
            .Where(e => e.Message.Equals("User not found"));
    }

    [Fact]
    public async Task Error_Invalid_User()
    {
        (var user, var password) = UserBuild.Build();
        password += "a";

        var service = CreateService(user);

        Func<Task> act = async () => { await service.ExecuteLogin(new UserLoginDto(user.Email, password)); };

        await act.Should().ThrowAsync<InvalidLoginException>()
            .Where(e => e.Message.Equals("Email or password invalid"));
    }

    private static LoginService CreateService(User? user = null)
    {
        var userRepositoryBuilder = new UserRepositoryBuilder();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var acessTokenGenerator = JwtTokenGeneratorBuilder.Build(userRepositoryBuilder.Build());

        if (user is not null)
        {
            userRepositoryBuilder.GetByEmailAndPassword(user.Email, user.Password, user);
            userRepositoryBuilder.GetUserByEmail(user.Email, user);
        }

        return new LoginService(userRepositoryBuilder.Build(), passwordEncripter, acessTokenGenerator);
    }

}

