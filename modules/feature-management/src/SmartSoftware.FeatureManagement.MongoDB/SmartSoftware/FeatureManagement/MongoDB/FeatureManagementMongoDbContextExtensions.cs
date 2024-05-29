using SmartSoftware.MongoDB;

namespace SmartSoftware.FeatureManagement.MongoDB;

public static class FeatureManagementMongoDbContextExtensions
{
    public static void ConfigureFeatureManagement(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<FeatureGroupDefinitionRecord>(b =>
        {
            b.CollectionName = SmartSoftwareFeatureManagementDbProperties.DbTablePrefix + "FeatureGroups";
        });

        builder.Entity<FeatureDefinitionRecord>(b =>
        {
            b.CollectionName = SmartSoftwareFeatureManagementDbProperties.DbTablePrefix + "Features";
        });

        builder.Entity<FeatureValue>(b =>
        {
            b.CollectionName = SmartSoftwareFeatureManagementDbProperties.DbTablePrefix + "FeatureValues";
        });
    }
}
