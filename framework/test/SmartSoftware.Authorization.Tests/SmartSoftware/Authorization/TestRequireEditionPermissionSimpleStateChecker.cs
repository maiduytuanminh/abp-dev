using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.Security.Claims;
using SmartSoftware.SimpleStateChecking;

namespace SmartSoftware.Authorization;

public class TestRequireEditionPermissionSimpleStateChecker : ISimpleStateChecker<PermissionDefinition>
{
    public Task<bool> IsEnabledAsync(SimpleStateCheckerContext<PermissionDefinition> context)
    {
        var currentPrincipalAccessor = context.ServiceProvider.GetRequiredService<ICurrentPrincipalAccessor>();
        return Task.FromResult(currentPrincipalAccessor.Principal?.FindEditionId() != null);
    }
}
