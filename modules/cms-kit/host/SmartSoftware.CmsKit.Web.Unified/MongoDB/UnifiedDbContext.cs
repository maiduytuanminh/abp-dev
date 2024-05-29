#if MongoDB
using SmartSoftware.AuditLogging.MongoDB;
using SmartSoftware.BlobStoring.Database.MongoDB;
using SmartSoftware.FeatureManagement.MongoDB;
using SmartSoftware.Identity.MongoDB;
using SmartSoftware.MongoDB;
using SmartSoftware.PermissionManagement.MongoDB;
using SmartSoftware.SettingManagement.MongoDB;
using SmartSoftware.TenantManagement.MongoDB;

namespace SmartSoftware.CmsKit.MongoDB;

public class UnifiedDbContext : SmartSoftwareMongoDbContext
{
    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigurePermissionManagement();
        modelBuilder.ConfigureSettingManagement();
        modelBuilder.ConfigureAuditLogging();
        modelBuilder.ConfigureIdentity();
        modelBuilder.ConfigureTenantManagement();
        modelBuilder.ConfigureFeatureManagement();
        modelBuilder.ConfigureCmsKit();
        modelBuilder.ConfigureBlobStoring();
    }
}
#endif
