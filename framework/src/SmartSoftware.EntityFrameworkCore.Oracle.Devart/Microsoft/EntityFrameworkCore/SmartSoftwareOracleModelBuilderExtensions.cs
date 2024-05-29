using SmartSoftware.EntityFrameworkCore;

namespace Microsoft.EntityFrameworkCore;

public static class SmartSoftwareOracleModelBuilderExtensions
{
    public static void UseOracle(
        this ModelBuilder modelBuilder)
    {
        modelBuilder.SetDatabaseProvider(EfCoreDatabaseProvider.Oracle);
    }
}
