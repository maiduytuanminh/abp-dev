using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.SettingManagement.MongoDB;

[IgnoreMultiTenancy]
[ConnectionStringName(SmartSoftwareSettingManagementDbProperties.ConnectionStringName)]
public class SettingManagementMongoDbContext : SmartSoftwareMongoDbContext, ISettingManagementMongoDbContext
{
    public IMongoCollection<Setting> Settings => Collection<Setting>();
    public IMongoCollection<SettingDefinitionRecord> SettingDefinitionRecords => Collection<SettingDefinitionRecord>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureSettingManagement();
    }
}
