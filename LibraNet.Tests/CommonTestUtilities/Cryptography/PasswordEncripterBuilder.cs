using LibraNet.Services.Cryptography;

namespace LibraNet.Tests.CommonTestUtilities.Cryptography;
public class PasswordEncripterBuilder
{
    public static IPasswordEncripter Build() => new PasswordEncripter("chave-adicional-top-secret");
}

