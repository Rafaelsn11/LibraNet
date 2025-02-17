namespace LibraNet.Security.Tokens.Interfaces;

public interface IAccessTokenGenerator
{
    public Task<string> Generate(Guid userIdentifier);
}
