using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.IdentityServer.Clients;
using SmartSoftware.IdentityServer.Devices;
using SmartSoftware.IdentityServer.Grants;

namespace SmartSoftware.IdentityServer;

public static class IdentityServerBuilderExtensions
{
    public static IIdentityServerBuilder AddSmartSoftwareStores(this IIdentityServerBuilder builder)
    {
        builder.Services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();
        builder.Services.AddTransient<IDeviceFlowStore, DeviceFlowStore>();

        return builder
            .AddClientStore<ClientStore>()
            .AddResourceStore<ResourceStore>()
            .AddCorsPolicyService<SmartSoftwareCorsPolicyService>();
    }
}
