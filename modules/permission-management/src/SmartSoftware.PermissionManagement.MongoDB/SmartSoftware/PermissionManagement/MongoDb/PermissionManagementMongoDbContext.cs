using MongoDB.Driver;
using SmartSoftware.Data;
using SmartSoftware.MongoDB;

namespace SmartSoftware.PermissionManagement.MongoDB;

[ConnectionStringName(SmartSoftwarePermissionManagementDbProperties.ConnectionStringName)]
public class PermissionManagementMongoDbContext : SmartSoftwareMongoDbContext, IPermissionManagementMongoDbContext
{
    public IMongoCollection<PermissionGroupDefinitionRecord> PermissionGroups => Collection<PermissionGroupDefinitionRecord>();
    public IMongoCollection<PermissionDefinitionRecord> Permissions => Collection<PermissionDefinitionRecord>();
    public IMongoCollection<PermissionGrant> PermissionGrants => Collection<PermissionGrant>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigurePermissionManagement();
    }
}
