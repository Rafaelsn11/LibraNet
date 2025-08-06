using LibraNet.Repository.Interfaces;
using LibraNet.Security.Tokens.Access.Generator;
using LibraNet.Security.Tokens.Interfaces;

namespace LibraNet.Tests.CommonTestUtilities.Tokens;

public class JwtTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build(IUserRepository repository)
        => new JwtTokenGenerator(expirationTimeMinutes: 5, signingkey: "tttttttttttttttttttttttttttttttt", repository);
}

