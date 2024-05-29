using SmartSoftware.MongoDB;

namespace SmartSoftware.TenantManagement.MongoDB;

public static class SmartSoftwareTenantManagementMongoDbContextExtensions
{
    public static void ConfigureTenantManagement(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Tenant>(b =>
        {
            b.CollectionName = SmartSoftwareTenantManagementDbProperties.DbTablePrefix + "Tenants";
        });
    }
}
