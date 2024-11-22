namespace LibraNet.Exceptions.ResponseError;

public class ResponseErrorsJson
{
    public ResponseErrorsJson(IList<string> errors)
    {
        Errors = errors;
    }
    public IList<string> Errors { get; set; } = [];
}
