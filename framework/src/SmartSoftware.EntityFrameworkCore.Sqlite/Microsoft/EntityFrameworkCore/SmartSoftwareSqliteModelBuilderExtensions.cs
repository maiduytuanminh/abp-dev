using SmartSoftware.EntityFrameworkCore;

namespace Microsoft.EntityFrameworkCore;

public static class SmartSoftwareSqliteModelBuilderExtensions
{
    public static void UseSqlite(
        this ModelBuilder modelBuilder)
    {
        modelBuilder.SetDatabaseProvider(EfCoreDatabaseProvider.Sqlite);
    }
}
