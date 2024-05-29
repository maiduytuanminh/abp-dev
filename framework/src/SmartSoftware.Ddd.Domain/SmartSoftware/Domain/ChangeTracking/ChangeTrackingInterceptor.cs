using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.DynamicProxy;

namespace SmartSoftware.Domain.ChangeTracking;

public class ChangeTrackingInterceptor : SmartSoftwareInterceptor, ITransientDependency
{
    private readonly IEntityChangeTrackingProvider _entityChangeTrackingProvider;

    public ChangeTrackingInterceptor(IEntityChangeTrackingProvider entityChangeTrackingProvider)
    {
        _entityChangeTrackingProvider = entityChangeTrackingProvider;
    }

    public async override Task InterceptAsync(ISmartSoftwareMethodInvocation invocation)
    {
        if (!ChangeTrackingHelper.IsEntityChangeTrackingMethod(invocation.Method, out var changeTrackingAttribute))
        {
            await invocation.ProceedAsync();
            return;
        }

        using (_entityChangeTrackingProvider.Change(changeTrackingAttribute?.IsEnabled))
        {
            await invocation.ProceedAsync();
        }
    }
}
