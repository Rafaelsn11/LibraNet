namespace LibraNet.Security.Tokens.Interfaces;

public interface IAccessTokenValidator
{
    public Guid ValidateAndGetUserIdentifier(string token);
}
