using System.Net;

namespace LibraNet.Exceptions.ExceptionsBase;

public class ErrorOrValidationException : LibraException
{
    private readonly IList<string> _errors;
    public ErrorOrValidationException(IList<string> messages) : base(string.Empty)
    {
        _errors = messages;
    }
    public override IList<string> GetErrorMessages()
        => _errors;

    public override HttpStatusCode GetStatusCode()
        => HttpStatusCode.BadRequest;
}
