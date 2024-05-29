using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Aspects;
using SmartSoftware.DependencyInjection;
using SmartSoftware.DynamicProxy;

namespace SmartSoftware.Features;

public class FeatureInterceptor : SmartSoftwareInterceptor, ITransientDependency
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public FeatureInterceptor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public override async Task InterceptAsync(ISmartSoftwareMethodInvocation invocation)
    {
        if (SmartSoftwareCrossCuttingConcerns.IsApplied(invocation.TargetObject, SmartSoftwareCrossCuttingConcerns.FeatureChecking))
        {
            await invocation.ProceedAsync();
            return;
        }

        await CheckFeaturesAsync(invocation);
        await invocation.ProceedAsync();
    }

    protected virtual async Task CheckFeaturesAsync(ISmartSoftwareMethodInvocation invocation)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            await scope.ServiceProvider.GetRequiredService<IMethodInvocationFeatureCheckerService>().CheckAsync(
                new MethodInvocationFeatureCheckerContext(
                    invocation.Method
                )
            );
        }
    }
}
