using SmartSoftware.IdentityServer.ApiResources;
using SmartSoftware.IdentityServer.ApiScopes;
using SmartSoftware.IdentityServer.Clients;
using SmartSoftware.IdentityServer.Devices;
using SmartSoftware.IdentityServer.Grants;
using SmartSoftware.IdentityServer.IdentityResources;
using SmartSoftware.MongoDB;

namespace SmartSoftware.IdentityServer.MongoDB;

public static class SmartSoftwareIdentityServerMongoDbContextExtensions
{
    public static void ConfigureIdentityServer(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<ApiResource>(b =>
        {
            b.CollectionName = SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "ApiResources";
        });

        builder.Entity<ApiScope>(b =>
        {
            b.CollectionName = SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "ApiScopes";
        });

        builder.Entity<IdentityResource>(b =>
        {
            b.CollectionName = SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "IdentityResources";
        });

        builder.Entity<Client>(b =>
        {
            b.CollectionName = SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "Clients";
        });

        builder.Entity<PersistedGrant>(b =>
        {
            b.CollectionName = SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "PersistedGrants";
        });

        builder.Entity<DeviceFlowCodes>(b =>
        {
            b.CollectionName = SmartSoftwareIdentityServerDbProperties.DbTablePrefix + "DeviceFlowCodes";
        });
    }
}
