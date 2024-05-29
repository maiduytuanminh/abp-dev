using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.FeatureManagement.MongoDB;

[IgnoreMultiTenancy]
[ConnectionStringName(SmartSoftwareFeatureManagementDbProperties.ConnectionStringName)]
public class FeatureManagementMongoDbContext : SmartSoftwareMongoDbContext, IFeatureManagementMongoDbContext
{
    public IMongoCollection<FeatureGroupDefinitionRecord> FeatureGroups => Collection<FeatureGroupDefinitionRecord>();
    public IMongoCollection<FeatureDefinitionRecord> Features => Collection<FeatureDefinitionRecord>();
    public IMongoCollection<FeatureValue> FeatureValues => Collection<FeatureValue>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureFeatureManagement();
    }
}
