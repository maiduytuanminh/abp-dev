using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;

namespace SmartSoftware.PermissionManagement.MongoDB;

[ConnectionStringName(SmartSoftwarePermissionManagementDbProperties.ConnectionStringName)]
public interface IPermissionManagementMongoDbContext : ISmartSoftwareMongoDbContext
{
    IMongoCollection<PermissionGroupDefinitionRecord> PermissionGroups { get; }
    
    IMongoCollection<PermissionDefinitionRecord> Permissions { get; }
    
    IMongoCollection<PermissionGrant> PermissionGrants { get; }
}
