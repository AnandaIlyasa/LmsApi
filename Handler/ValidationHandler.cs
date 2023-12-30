using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LmsApi.Handler;

public class ValidationHandler : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid == false)
        {
            context.Result = new BadRequestObjectResult(
                new { message = context.ModelState.Values.Select(op => op.Errors.First().ErrorMessage) }
            );
        }
    }
}
