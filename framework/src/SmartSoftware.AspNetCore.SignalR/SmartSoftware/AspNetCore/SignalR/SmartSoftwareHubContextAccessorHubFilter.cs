using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace SmartSoftware.AspNetCore.SignalR;

public class SmartSoftwareHubContextAccessorHubFilter : IHubFilter
{
    public virtual async ValueTask<object?> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object?>> next)
    {
        var hubContextAccessor = invocationContext.ServiceProvider.GetRequiredService<ISmartSoftwareHubContextAccessor>();
        using (hubContextAccessor.Change(new SmartSoftwareHubContext(
                   invocationContext.ServiceProvider,
                   invocationContext.Hub,
                   invocationContext.HubMethod,
                   invocationContext.HubMethodArguments)))
        {
            return await next(invocationContext);
        }
    }
}
