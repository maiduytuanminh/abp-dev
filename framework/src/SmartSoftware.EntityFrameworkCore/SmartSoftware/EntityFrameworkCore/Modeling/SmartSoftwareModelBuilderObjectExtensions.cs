using Microsoft.EntityFrameworkCore;
using SmartSoftware.ObjectExtending;

namespace SmartSoftware.EntityFrameworkCore.Modeling;

public static class SmartSoftwareModelBuilderObjectExtensions
{
    public static void TryConfigureObjectExtensions<TDbContext>(this ModelBuilder modelBuilder)
        where TDbContext : DbContext
    {
        ObjectExtensionManager.Instance.ConfigureEfCoreDbContext<TDbContext>(modelBuilder);
    }
}
