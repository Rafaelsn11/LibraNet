using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LibraNet.Models.Entities;
using LibraNet.Repository.Interfaces;
using LibraNet.Security.Tokens.Interfaces;

namespace LibraNet.Services.LoggedUser;

public class LoggedUser : ILoggedUser
{
    private readonly IUserRepository _repository;
    private readonly ITokenProvider _tokenProvider;
    public LoggedUser(IUserRepository repository, ITokenProvider tokenProvider)
    {
        _repository = repository;
        _tokenProvider = tokenProvider;
    }

    public async Task<User> User()
    {
        var token = _tokenProvider.Value();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        var userIdentifier = Guid.Parse(identifier);

        return await _repository.GetActiveUserByIdentifierAsync(userIdentifier);
    }
}
