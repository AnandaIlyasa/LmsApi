using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LmsApi.Handler;

public class ErrorHandler : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is UnauthorizedAccessException)
        {
            context.Result = new UnauthorizedObjectResult(new { message = context.Exception.Message });
        }
        else
        {
            context.Result = new BadRequestObjectResult(new { message = context.Exception.Message });
        }
    }
}
