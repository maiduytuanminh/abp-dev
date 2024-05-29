using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.SettingManagement.MongoDB;

[IgnoreMultiTenancy]
[ConnectionStringName(SmartSoftwareSettingManagementDbProperties.ConnectionStringName)]
public interface ISettingManagementMongoDbContext : ISmartSoftwareMongoDbContext
{
    IMongoCollection<Setting> Settings { get; }
    IMongoCollection<SettingDefinitionRecord> SettingDefinitionRecords { get; }
}
