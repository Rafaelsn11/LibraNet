using LibraNet.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LibraNet.Attributes;

public class AdminOnlyAttribute : TypeFilterAttribute
{
    public AdminOnlyAttribute() : base(typeof(RoleBasedAuthorizationFilter))
    {
        Arguments = new object[] { new string[] { "Admin" } };
    }
}
