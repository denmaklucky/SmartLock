using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApi.Responses;

namespace WebApi.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception e)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            var errorResponse = new ErrorResponse("InternalServerError", e.Message);
             var settings = new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};
             var json = JsonConvert.SerializeObject(errorResponse, Formatting.Indented, settings);

            await context.Response.WriteAsync(json);
        }
    }
}