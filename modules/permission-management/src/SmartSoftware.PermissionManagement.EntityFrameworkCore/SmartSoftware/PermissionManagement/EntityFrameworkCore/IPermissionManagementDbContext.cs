using Microsoft.EntityFrameworkCore;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;

namespace SmartSoftware.PermissionManagement.EntityFrameworkCore;

[ConnectionStringName(SmartSoftwarePermissionManagementDbProperties.ConnectionStringName)]
public interface IPermissionManagementDbContext : IEfCoreDbContext
{
    DbSet<PermissionGroupDefinitionRecord> PermissionGroups { get; }
    
    DbSet<PermissionDefinitionRecord> Permissions { get; }
    
    DbSet<PermissionGrant> PermissionGrants { get; }
}
