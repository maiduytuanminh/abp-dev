using Microsoft.EntityFrameworkCore;
using SmartSoftware.AuditLogging.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.FeatureManagement.EntityFrameworkCore;
using SmartSoftware.Identity.EntityFrameworkCore;
using SmartSoftware.OpenIddict.EntityFrameworkCore;
using SmartSoftware.PermissionManagement.EntityFrameworkCore;
using SmartSoftware.SettingManagement.EntityFrameworkCore;
using SmartSoftware.TenantManagement.EntityFrameworkCore;

namespace MyCompanyName.MyProjectName.Data;

public class MyProjectNameDbContext : SmartSoftwareDbContext<MyProjectNameDbContext>
{
    public MyProjectNameDbContext(DbContextOptions<MyProjectNameDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own entities here */
    }
}
