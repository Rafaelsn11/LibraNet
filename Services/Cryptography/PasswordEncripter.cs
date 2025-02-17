using System.Security.Cryptography;
using System.Text;

namespace LibraNet.Services.Cryptography;

public class PasswordEncripter : IPasswordEncripter
{
    private readonly string _additionalKey;
    
    public PasswordEncripter(string additionalKey) => _additionalKey = additionalKey;

    public string Encrypt(string password, string salt)
    {
        var newPassword = $"{password}{salt}{_additionalKey}";

        var bytes = Encoding.UTF8.GetBytes(newPassword);
        var hashBytes = SHA512.HashData(bytes);

        return StringBytes(hashBytes);
    }

    public string GenerateSalt(int size = 16)
    {
        var randomBytes = new byte[size];

        using(var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        return Convert.ToBase64String(randomBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (byte b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }

        return sb.ToString();
    }
}
