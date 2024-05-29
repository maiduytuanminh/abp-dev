using Microsoft.EntityFrameworkCore;

namespace SmartSoftware.EntityFrameworkCore.TestApp.ThirdDbContext;

/* This dbcontext is just for testing to replace dbcontext from the application using SmartSoftwareDbContextRegistrationOptions.ReplaceDbContext
 */
public class ThirdDbContext : SmartSoftwareDbContext<ThirdDbContext>, IThirdDbContext
{
    public DbSet<ThirdDbContextDummyEntity> DummyEntities { get; set; }

    public ThirdDbContext(DbContextOptions<ThirdDbContext> options)
        : base(options)
    {
    }
}
