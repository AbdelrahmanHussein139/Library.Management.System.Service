using Microsoft.AspNetCore.Mvc.Filters;


namespace Library.Management.System.Web.Api.Filters;

public class LogFilter : IActionFilter
{
    private readonly ILogger<LogFilter> _logger;

    public LogFilter(ILogger<LogFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Log incoming request info
        var controllerName = context.Controller?.GetType().Name;
        var actionName = context.ActionDescriptor.DisplayName;

        _logger.LogInformation("Executing {Controller}.{Action} with arguments {Arguments}",
            controllerName, actionName,
            context.ActionArguments.Count > 0 ? context.ActionArguments : "No arguments");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var controllerName = context.Controller?.GetType().Name;
        var actionName = context.ActionDescriptor.DisplayName;

        if (context.Exception != null)
        {
            // Log unhandled exceptions
            _logger.LogError(context.Exception, "Exception in {Controller}.{Action}", controllerName, actionName);
        }
        else
        {
            _logger.LogInformation("Executed {Controller}.{Action} successfully", controllerName, actionName);
        }
    }
}
