using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Security.Claims;
using SmartSoftware.SimpleStateChecking;

namespace SmartSoftware.Authorization;

public class TestGlobalRequireRolePermissionSimpleStateChecker : ISimpleStateChecker<PermissionDefinition>, ITransientDependency
{
    public Task<bool> IsEnabledAsync(SimpleStateCheckerContext<PermissionDefinition> context)
    {
        var currentPrincipalAccessor = context.ServiceProvider.GetRequiredService<ICurrentPrincipalAccessor>();
        return Task.FromResult(currentPrincipalAccessor.Principal != null && currentPrincipalAccessor.Principal.IsInRole("admin"));
    }
}
