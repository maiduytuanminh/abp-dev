using Microsoft.EntityFrameworkCore;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.FeatureManagement.EntityFrameworkCore;
using SmartSoftware.Identity.EntityFrameworkCore;
using SmartSoftware.OpenIddict.EntityFrameworkCore;
using SmartSoftware.PermissionManagement.EntityFrameworkCore;
using SmartSoftware.SettingManagement.EntityFrameworkCore;
using SmartSoftware.TenantManagement.EntityFrameworkCore;

namespace OpenIddict.Demo.Server.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class ServerDbContext : SmartSoftwareDbContext<ServerDbContext>
{
   public ServerDbContext(DbContextOptions<ServerDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureIdentity();
        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();
        
        builder.ConfigureOpenIddict();
    }
}
