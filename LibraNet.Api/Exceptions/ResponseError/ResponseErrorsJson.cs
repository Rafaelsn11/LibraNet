namespace LibraNet.Exceptions.ResponseError;

public class ResponseErrorsJson
{
    public IList<string> Errors { get; set; } = [];
    public bool TokenExpired { get; set; }

    public ResponseErrorsJson(IList<string> errors)
    {
        Errors = errors;
    }

    public ResponseErrorsJson(string error)
    {
        Errors = new List<string>
        {
            error
        };
    }
}
