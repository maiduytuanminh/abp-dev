using Microsoft.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.TestApp.ThirdDbContext;

namespace SmartSoftware.EntityFrameworkCore.TestApp.FourthContext;

/* This dbcontext is just for testing to replace dbcontext from the application using ReplaceDbContextAttribute
 */
public class FourthDbContext : SmartSoftwareDbContext<FourthDbContext>, IFourthDbContext
{
    public DbSet<FourthDbContextDummyEntity> FourthDummyEntities { get; set; }

    public FourthDbContext(DbContextOptions<FourthDbContext> options)
        : base(options)
    {
    }
}
