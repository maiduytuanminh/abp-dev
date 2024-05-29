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

public class GlobalFeaturePageFilter : IAsyncPageFilter, ISmartSoftwareFilter, ITransientDependency
{
    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        return Task.CompletedTask;
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        if (context.HandlerInstance == null || !context.ActionDescriptor.IsPageAction())
        {
            await next();
            return;
        }

        if (!GlobalFeatureHelper.IsGlobalFeatureEnabled(context.HandlerInstance.GetType(), out var attribute))
        {
            var logger = context.GetService<ILogger<GlobalFeatureActionFilter>>(NullLogger<GlobalFeatureActionFilter>.Instance)!;
            logger.LogWarning($"The '{context.HandlerInstance.GetType().FullName}' page needs to enable '{attribute!.Name}' feature.");
            context.Result = new NotFoundResult();
            return;
        }

        using (SmartSoftwareCrossCuttingConcerns.Applying(context.HandlerInstance, SmartSoftwareCrossCuttingConcerns.GlobalFeatureChecking))
        {
            await next();
        }
    }
}
