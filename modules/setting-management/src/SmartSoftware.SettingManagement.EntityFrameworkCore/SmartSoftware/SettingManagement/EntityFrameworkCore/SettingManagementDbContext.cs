using Microsoft.EntityFrameworkCore;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.SettingManagement.EntityFrameworkCore;

[IgnoreMultiTenancy]
[ConnectionStringName(SmartSoftwareSettingManagementDbProperties.ConnectionStringName)]
public class SettingManagementDbContext : SmartSoftwareDbContext<SettingManagementDbContext>, ISettingManagementDbContext
{
    public DbSet<Setting> Settings { get; set; }
    public DbSet<SettingDefinitionRecord> SettingDefinitionRecords { get; set; }

    public SettingManagementDbContext(DbContextOptions<SettingManagementDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureSettingManagement();
    }
}
