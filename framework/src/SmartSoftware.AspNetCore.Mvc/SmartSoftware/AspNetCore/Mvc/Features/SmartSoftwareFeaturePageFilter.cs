using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartSoftware.Aspects;
using SmartSoftware.AspNetCore.Filters;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Features;

namespace SmartSoftware.AspNetCore.Mvc.Features;

public class SmartSoftwareFeaturePageFilter : IAsyncPageFilter, ISmartSoftwareFilter, ITransientDependency
{
    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        return Task.CompletedTask;
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        if (context.HandlerMethod == null || !context.ActionDescriptor.IsPageAction())
        {
            await next();
            return;
        }

        var methodInfo = context.HandlerMethod.MethodInfo;

        using (SmartSoftwareCrossCuttingConcerns.Applying(context.HandlerInstance, SmartSoftwareCrossCuttingConcerns.FeatureChecking))
        {
            var methodInvocationFeatureCheckerService = context.GetRequiredService<IMethodInvocationFeatureCheckerService>();
            await methodInvocationFeatureCheckerService.CheckAsync(new MethodInvocationFeatureCheckerContext(methodInfo));

            await next();
        }
    }
}
