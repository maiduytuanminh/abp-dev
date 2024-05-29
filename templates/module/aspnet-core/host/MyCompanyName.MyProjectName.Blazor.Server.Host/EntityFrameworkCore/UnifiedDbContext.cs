using Microsoft.EntityFrameworkCore;
using MyCompanyName.MyProjectName.EntityFrameworkCore;
using SmartSoftware.AuditLogging.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.FeatureManagement.EntityFrameworkCore;
using SmartSoftware.Identity.EntityFrameworkCore;
using SmartSoftware.PermissionManagement.EntityFrameworkCore;
using SmartSoftware.SettingManagement.EntityFrameworkCore;
using SmartSoftware.TenantManagement.EntityFrameworkCore;

namespace MyCompanyName.MyProjectName.Blazor.Server.Host.EntityFrameworkCore;

public class UnifiedDbContext : SmartSoftwareDbContext<UnifiedDbContext>
{
    public UnifiedDbContext(DbContextOptions<UnifiedDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigurePermissionManagement();
        modelBuilder.ConfigureSettingManagement();
        modelBuilder.ConfigureAuditLogging();
        modelBuilder.ConfigureIdentity();
        modelBuilder.ConfigureFeatureManagement();
        modelBuilder.ConfigureTenantManagement();
        modelBuilder.ConfigureMyProjectName();
    }
}
