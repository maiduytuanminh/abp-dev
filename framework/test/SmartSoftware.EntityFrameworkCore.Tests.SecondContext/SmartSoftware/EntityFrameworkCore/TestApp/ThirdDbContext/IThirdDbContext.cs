using Microsoft.EntityFrameworkCore;

namespace SmartSoftware.EntityFrameworkCore.TestApp.ThirdDbContext;

public interface IThirdDbContext : IEfCoreDbContext
{
    DbSet<ThirdDbContextDummyEntity> DummyEntities { get; set; }
}
