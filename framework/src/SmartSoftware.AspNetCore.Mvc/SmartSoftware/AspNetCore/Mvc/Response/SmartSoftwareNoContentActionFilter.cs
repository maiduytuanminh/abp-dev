using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartSoftware.AspNetCore.Filters;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Mvc.Response;

public class SmartSoftwareNoContentActionFilter : IAsyncActionFilter, ISmartSoftwareFilter, ITransientDependency
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionDescriptor.IsControllerAction())
        {
            await next();
            return;
        }

        await next();

        if (!context.HttpContext.Response.HasStarted &&
            context.HttpContext.Response.StatusCode == (int)HttpStatusCode.OK &&
            context.Result == null)
        {
            var returnType = context.ActionDescriptor.GetReturnType();
            if (returnType == typeof(Task) || returnType == typeof(void))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            }
        }
    }
}
