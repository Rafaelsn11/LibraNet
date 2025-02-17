using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LibraNet.Security.Tokens.Access;

public abstract class JwtTokenHandler
{
    protected SymmetricSecurityKey SecurityKey(string signingkey)
    {
        var bytes = Encoding.UTF8.GetBytes(signingkey);

        return new SymmetricSecurityKey(bytes);
    }
}
