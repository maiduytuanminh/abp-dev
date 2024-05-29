using Microsoft.EntityFrameworkCore;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.SettingManagement.EntityFrameworkCore;

[IgnoreMultiTenancy]
[ConnectionStringName(SmartSoftwareSettingManagementDbProperties.ConnectionStringName)]
public interface ISettingManagementDbContext : IEfCoreDbContext
{
    DbSet<Setting> Settings { get; }

    DbSet<SettingDefinitionRecord> SettingDefinitionRecords { get; }
}
