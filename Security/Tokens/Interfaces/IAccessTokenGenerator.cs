namespace LibraNet.Security.Tokens.Interfaces;

public interface IAccessTokenGenerator
{
    public string Generate(Guid userIdentifier);
}
