using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.FeatureManagement.MongoDB;

[IgnoreMultiTenancy]
[ConnectionStringName(SmartSoftwareFeatureManagementDbProperties.ConnectionStringName)]
public interface IFeatureManagementMongoDbContext : ISmartSoftwareMongoDbContext
{
    IMongoCollection<FeatureGroupDefinitionRecord> FeatureGroups { get; }

    IMongoCollection<FeatureDefinitionRecord> Features { get; }

    IMongoCollection<FeatureValue> FeatureValues { get; }
}
