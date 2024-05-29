using SmartSoftware.MongoDB;

namespace SmartSoftware.SettingManagement.MongoDB;

public static class SettingManagementMongoDbContextExtensions
{
    public static void ConfigureSettingManagement(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Setting>(b =>
        {
            b.CollectionName = SmartSoftwareSettingManagementDbProperties.DbTablePrefix + "Settings";
        });

        builder.Entity<SettingDefinitionRecord>(b =>
        {
            b.CollectionName = SmartSoftwareSettingManagementDbProperties.DbTablePrefix + "SettingDefinitions";
        });
    }
}
