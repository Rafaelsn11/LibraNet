using System.Net;

namespace LibraNet.Exceptions.ExceptionsBase;

public class NotFoundException : LibraException
{
    public NotFoundException(string message) : base(message)
    {

    }
    public override IList<string> GetErrorMessages()
        => new List<string>()
            {
                Message
            };

    public override HttpStatusCode GetStatusCode()
        => HttpStatusCode.NotFound;
}
