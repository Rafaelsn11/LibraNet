using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LibraNet.Repository.Interfaces;
using LibraNet.Security.Tokens.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace LibraNet.Security.Tokens.Access.Generator;

public class JwtTokenGenerator : JwtTokenHandler, IAccessTokenGenerator
{
    private readonly uint _expirationTimeMinutes;
    private readonly string _signingkey;
    private readonly IUserRepository _repository;

    public JwtTokenGenerator(uint expirationTimeMinutes, string signingkey, IUserRepository repository)
    {
        _expirationTimeMinutes = expirationTimeMinutes;
        _signingkey = signingkey;
        _repository = repository;
    }

    public async Task<string> Generate(Guid userIdentifier)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Sid, userIdentifier.ToString())
        };

        var userRoles = await _repository.GetUserRolesByIdentifier(userIdentifier.ToString());

        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(_signingkey), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }
}

