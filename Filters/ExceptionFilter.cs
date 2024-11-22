using LibraNet.Exceptions.ExceptionsBase;
using LibraNet.Exceptions.ResponseError;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraNet.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is LibraException)
        {
            var libraException = (LibraException)context.Exception;

            context.HttpContext.Response.StatusCode = (int)libraException.GetStatusCode();

            var responseJson = new ResponseErrorsJson(libraException.GetErrorMessages());

            context.Result = new ObjectResult(responseJson);
        }
        else
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var list = new List<string>
            {
                "Erro desconhecido"
            };

            var responseJson = new ResponseErrorsJson(list);

            context.Result = new ObjectResult(responseJson);
        }
    }
}
