using LibraNet.Filters;
using Microsoft.AspNetCore.Mvc;


namespace LibraNet.Attributes;

public class AuthenticatedUserAttribute : TypeFilterAttribute
{
    public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
    {
    }
}
