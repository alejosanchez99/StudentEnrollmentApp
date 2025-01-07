
namespace StudentEnrollment.Api.Filters;

public class LoggingFilter(ILoggerFactory loggerFactory) : IEndpointFilter
{
    private readonly ILogger logger = loggerFactory.CreateLogger<LoggingFilter>();

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        string method = context.HttpContext.Request.Method;
        string path = context.HttpContext.Request.Path;

        logger.LogInformation("{method} request made to {path}", method, path);

        try
        {
            return await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
            return Results.Problem("An Error has occured, please try again");
        }
    }
}

