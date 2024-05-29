using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using SmartSoftware.Aspects;
using SmartSoftware.AspNetCore.Filters;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Features;

namespace SmartSoftware.AspNetCore.Mvc.Features;

public class SmartSoftwareFeatureActionFilter : IAsyncActionFilter, ISmartSoftwareFilter, ITransientDependency
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionDescriptor.IsControllerAction())
        {
            await next();
            return;
        }

        var methodInfo = context.ActionDescriptor.GetMethodInfo();

        using (SmartSoftwareCrossCuttingConcerns.Applying(context.Controller, SmartSoftwareCrossCuttingConcerns.FeatureChecking))
        {
            var methodInvocationFeatureCheckerService = context.GetRequiredService<IMethodInvocationFeatureCheckerService>();
            await methodInvocationFeatureCheckerService.CheckAsync(new MethodInvocationFeatureCheckerContext(methodInfo));

            await next();
        }
    }
}
