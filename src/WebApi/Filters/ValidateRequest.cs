using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Responses;

namespace WebApi.Filters;

public class ValidateRequest : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.SelectMany(keyValuePair => keyValuePair.Value.Errors).Select(modelError => modelError.ErrorMessage).ToArray();
            var errorResponse = new ErrorResponse("InvalidRequest", errors);
            context.Result = new BadRequestObjectResult(errorResponse);
        }
    }
}