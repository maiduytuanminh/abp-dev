using SmartSoftware.EntityFrameworkCore;

namespace Microsoft.EntityFrameworkCore;

public static class SmartSoftwarePostgreSqlModelBuilderExtensions
{
    public static void UsePostgreSql(
        this ModelBuilder modelBuilder)
    {
        modelBuilder.SetDatabaseProvider(EfCoreDatabaseProvider.PostgreSql);
    }
}
