using System.Net;

namespace LibraNet.Exceptions.ExceptionsBase;

public class ResourceConflictException : LibraException
{
    public ResourceConflictException(string message) : base(message)
    {
    }

    public override IList<string> GetErrorMessages()
        => new List<string>()
            {
                Message
            };

    public override HttpStatusCode GetStatusCode()
        => HttpStatusCode.Conflict;
}
