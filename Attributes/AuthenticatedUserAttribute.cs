using LibraNet.Filters;
using Microsoft.AspNetCore.Mvc;


namespace LibraNet.Attributes;

public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(RoleBasedAuthorizationFilter))
    {
        Arguments = new object[] { new string[] { "Admin", "User" } };
    }
}
