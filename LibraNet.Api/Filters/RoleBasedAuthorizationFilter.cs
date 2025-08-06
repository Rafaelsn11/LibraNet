using LibraNet.Exceptions.ExceptionsAttibutes;
using LibraNet.Exceptions.ResponseError;
using LibraNet.Repository.Interfaces;
using LibraNet.Security.Tokens.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace LibraNet.Filters;

public class RoleBasedAuthorizationFilter : IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator _accessTokenValidator;
    private readonly IUserRepository _repository;
    private readonly string[] _allowedRoles;

    public RoleBasedAuthorizationFilter(
        IAccessTokenValidator accessTokenValidator,
        IUserRepository repository,
        string[] allowedRoles) 
    {
        _accessTokenValidator = accessTokenValidator;
        _repository = repository;
        _allowedRoles = allowedRoles;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);

            var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);

            var exist = await _repository.ExistsActiveUserWithIdentifier(userIdentifier);

            if (!exist)
                throw new LibraNetException("User without permission access");

            var roles = await _repository.GetUserRolesByIdentifier(userIdentifier.ToString());

            if (!_allowedRoles.Any(role => roles.Contains(role)))
                throw new LibraNetException("User is not authorized for this route");
        }
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorsJson("TokenIsExpired")
            {
                TokenExpired = true
            });
        }
        catch (LibraNetException ex)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorsJson(ex.Message));
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorsJson("User without permission access"));
        }
    }

    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();

        if(string.IsNullOrWhiteSpace(authentication))
            throw new LibraNetException("No Token");

        return authentication["Bearer ".Length..].Trim();
    }
}
