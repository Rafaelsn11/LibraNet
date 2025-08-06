using System.Net;

namespace LibraNet.Exceptions.ExceptionsBase;

public abstract class LibraException : Exception
{
    public LibraException(string message) : base(message)
    {

    }

    public abstract HttpStatusCode GetStatusCode();
    public abstract IList<string> GetErrorMessages();
}
