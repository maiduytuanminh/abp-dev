using Microsoft.EntityFrameworkCore;

namespace SmartSoftware.EntityFrameworkCore.TestApp.FourthContext;

public interface IFourthDbContext : IEfCoreDbContext
{
    DbSet<FourthDbContextDummyEntity> FourthDummyEntities { get; set; }
}
