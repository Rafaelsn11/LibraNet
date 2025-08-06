using System.Net;

namespace LibraNet.Exceptions.ExceptionsBase;

public class InvalidLoginException : LibraException
{
    public InvalidLoginException() : base("Email or password invalid")
    {
    }

    public override IList<string> GetErrorMessages()
        => new List<string>()
            {
                Message
            };

    public override HttpStatusCode GetStatusCode()
        => HttpStatusCode.Unauthorized;
}
