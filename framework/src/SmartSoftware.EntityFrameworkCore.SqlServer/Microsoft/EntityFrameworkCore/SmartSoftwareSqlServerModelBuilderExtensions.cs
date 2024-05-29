using SmartSoftware.EntityFrameworkCore;

namespace Microsoft.EntityFrameworkCore;

public static class SmartSoftwareSqlServerModelBuilderExtensions
{
    public static void UseSqlServer(
        this ModelBuilder modelBuilder)
    {
        modelBuilder.SetDatabaseProvider(EfCoreDatabaseProvider.SqlServer);
    }
}
