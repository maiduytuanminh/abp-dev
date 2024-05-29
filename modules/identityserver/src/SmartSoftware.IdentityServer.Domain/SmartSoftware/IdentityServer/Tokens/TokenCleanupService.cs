using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;
using SmartSoftware.IdentityServer.Devices;
using SmartSoftware.IdentityServer.Grants;
using SmartSoftware.Uow;

namespace SmartSoftware.IdentityServer.Tokens;

public class TokenCleanupService : ITransientDependency
{
    protected IPersistentGrantRepository PersistentGrantRepository { get; }
    protected IDeviceFlowCodesRepository DeviceFlowCodesRepository { get; }
    protected TokenCleanupOptions Options { get; }

    public TokenCleanupService(
        IPersistentGrantRepository persistentGrantRepository,
        IDeviceFlowCodesRepository deviceFlowCodesRepository,
        IOptions<TokenCleanupOptions> options)
    {
        PersistentGrantRepository = persistentGrantRepository;
        DeviceFlowCodesRepository = deviceFlowCodesRepository;
        Options = options.Value;
    }

    [UnitOfWork]
    public virtual async Task CleanAsync()
    {
        await RemoveGrantsAsync();
        await RemoveDeviceCodesAsync();
    }

    protected virtual async Task RemoveGrantsAsync()
    {
        await PersistentGrantRepository.DeleteExpirationAsync(DateTime.UtcNow);
    }

    protected virtual async Task RemoveDeviceCodesAsync()
    {
        await DeviceFlowCodesRepository.DeleteExpirationAsync(DateTime.UtcNow);
    }
}
