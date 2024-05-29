using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SmartSoftware.Aspects;
using SmartSoftware.AspNetCore.Filters;
using SmartSoftware.DependencyInjection;
using SmartSoftware.GlobalFeatures;

namespace SmartSoftware.AspNetCore.Mvc.GlobalFeatures;

public class GlobalFeatureActionFilter : IAsyncActionFilter, ISmartSoftwareFilter, ITransientDependency
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ActionDescriptor.IsControllerAction())
        {
            await next();
            return;
        }

        if (!GlobalFeatureHelper.IsGlobalFeatureEnabled(context.Controller.GetType(), out var attribute))
        {
            var logger = context.GetService<ILogger<GlobalFeatureActionFilter>>(NullLogger<GlobalFeatureActionFilter>.Instance)!;
            logger.LogWarning($"The '{context.Controller.GetType().FullName}' controller needs to enable '{attribute!.Name}' feature.");
            context.Result = new NotFoundResult();
            return;
        }

        using (SmartSoftwareCrossCuttingConcerns.Applying(context.Controller, SmartSoftwareCrossCuttingConcerns.GlobalFeatureChecking))
        {
            await next();
        }
    }
}
