using System.Threading.Tasks;
using SmartSoftware.Aspects;
using SmartSoftware.DependencyInjection;
using SmartSoftware.DynamicProxy;

namespace SmartSoftware.GlobalFeatures;

public class GlobalFeatureInterceptor : SmartSoftwareInterceptor, ITransientDependency
{
    public override async Task InterceptAsync(ISmartSoftwareMethodInvocation invocation)
    {
        if (SmartSoftwareCrossCuttingConcerns.IsApplied(invocation.TargetObject, SmartSoftwareCrossCuttingConcerns.GlobalFeatureChecking))
        {
            await invocation.ProceedAsync();
            return;
        }

        if (!GlobalFeatureHelper.IsGlobalFeatureEnabled(invocation.TargetObject.GetType(), out var attribute))
        {
            throw new SmartSoftwareGlobalFeatureNotEnabledException(code: SmartSoftwareGlobalFeatureErrorCodes.GlobalFeatureIsNotEnabled)
                .WithData("ServiceName", invocation.TargetObject.GetType().FullName!)
                .WithData("GlobalFeatureName", attribute!.Name!);
        }

        await invocation.ProceedAsync();
    }
}
