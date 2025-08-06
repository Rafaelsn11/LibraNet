using Bogus;
using LibraNet.Models.Entities;
using LibraNet.Tests.CommonTestUtilities.Cryptography;

namespace LibraNet.Tests.CommonTestUtilities.Models.Entities;

public class UserBuild
{
    public static (User user, string password) Build()
    {
        var passwordEncripter = PasswordEncripterBuilder.Build();

        var password = new Faker().Internet.Password();
        var salt = passwordEncripter.GenerateSalt();

        var user = new Faker<User>()
            .RuleFor(user => user.Id, () => 1)
            .RuleFor(user => user.Name, (f) => f.Person.FirstName)
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
            .RuleFor(user => user.UserIdentifier, _ => Guid.NewGuid())
            .RuleFor(user => user.Password, (f) => passwordEncripter.Encrypt(password, salt))
            .RuleFor(user => user.Salt, (f) => salt)
            .RuleFor(user => user.BirthDate, (f) => DateOnly.FromDateTime(f.Date.Past(30, DateTime.Today)))
            .RuleFor(user => user.IsActive, (f) => true);

        return (user, password);
    }
}

