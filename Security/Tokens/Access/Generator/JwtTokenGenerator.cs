using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LibraNet.Security.Tokens.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace LibraNet.Security.Tokens.Access.Generator;

public class JwtTokenGenerator : IAccessTokenGenerator
{
    private readonly uint _expirationTimeMinutes;
    private readonly string _signingkey;

    public JwtTokenGenerator(uint expirationTimeMinutes, string signingkey)
    {
        _expirationTimeMinutes = expirationTimeMinutes;
        _signingkey = signingkey;
    }

    public string Generate(Guid userIdentifier)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Sid, userIdentifier.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinutes),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(securityToken);
    }

    private SymmetricSecurityKey SecurityKey()
    {
        var bytes = Encoding.UTF8.GetBytes(_signingkey);

        return new SymmetricSecurityKey(bytes);
    }
}

