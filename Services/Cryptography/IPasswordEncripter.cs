namespace LibraNet.Services.Cryptography;

public interface IPasswordEncripter
{
    public string Encrypt(string password, string salt);
    public string GenerateSalt(int size = 16);
}
