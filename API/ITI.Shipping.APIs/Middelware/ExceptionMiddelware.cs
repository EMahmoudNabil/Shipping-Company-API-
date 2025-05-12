using ITI.Shipping.Core.Domin.ResponseHelper;
using System.Net;
using System.Text.Json;
namespace ITI.Shipping.APIs.Middelware;
public class ExceptionMiddelware
{
    private readonly RequestDelegate _next;
    public ExceptionMiddelware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            await HandleExceptionAsync(context,ex);
        }
    }
    private Task HandleExceptionAsync(HttpContext context,Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        var response = new ApiExceptions(context.Response.StatusCode,exception.Message,exception.StackTrace!);
        var jsonresponse = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(jsonresponse);
    }
}