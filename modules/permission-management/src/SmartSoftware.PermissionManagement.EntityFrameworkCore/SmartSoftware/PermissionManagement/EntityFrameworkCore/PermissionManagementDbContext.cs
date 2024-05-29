using Microsoft.EntityFrameworkCore;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;

namespace SmartSoftware.PermissionManagement.EntityFrameworkCore;

[ConnectionStringName(SmartSoftwarePermissionManagementDbProperties.ConnectionStringName)]
public class PermissionManagementDbContext : SmartSoftwareDbContext<PermissionManagementDbContext>, IPermissionManagementDbContext
{
    public DbSet<PermissionGroupDefinitionRecord> PermissionGroups { get; set; }
    public DbSet<PermissionDefinitionRecord> Permissions { get; set; }
    public DbSet<PermissionGrant> PermissionGrants { get; set; }

    public PermissionManagementDbContext(DbContextOptions<PermissionManagementDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigurePermissionManagement();
    }
}
