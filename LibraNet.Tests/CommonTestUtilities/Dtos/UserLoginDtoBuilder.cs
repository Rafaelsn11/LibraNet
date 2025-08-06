using Bogus;
using LibraNet.Models.Dtos.User;

namespace LibraNet.Tests.CommonTestUtilities.Dtos;

public class UserLoginDtoBuilder
{
    public static UserLoginDto Build()
    {
        var faker = new Faker();
        return new UserLoginDto(faker.Internet.Email(), faker.Internet.Password());
    }
}

