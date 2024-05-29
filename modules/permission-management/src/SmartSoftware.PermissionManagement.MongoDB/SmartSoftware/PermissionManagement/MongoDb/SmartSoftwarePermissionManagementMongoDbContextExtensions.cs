using SmartSoftware.MongoDB;

namespace SmartSoftware.PermissionManagement.MongoDB;

public static class SmartSoftwarePermissionManagementMongoDbContextExtensions
{
    public static void ConfigurePermissionManagement(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<PermissionGroupDefinitionRecord>(b =>
        {
            b.CollectionName = SmartSoftwarePermissionManagementDbProperties.DbTablePrefix + "PermissionGroups";
        });
        
        builder.Entity<PermissionDefinitionRecord>(b =>
        {
            b.CollectionName = SmartSoftwarePermissionManagementDbProperties.DbTablePrefix + "Permissions";
        });
        
        builder.Entity<PermissionGrant>(b =>
        {
            b.CollectionName = SmartSoftwarePermissionManagementDbProperties.DbTablePrefix + "PermissionGrants";
        });
    }
}
