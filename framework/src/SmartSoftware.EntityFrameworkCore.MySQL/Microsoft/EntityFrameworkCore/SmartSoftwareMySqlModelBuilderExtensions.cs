using SmartSoftware.EntityFrameworkCore;

namespace Microsoft.EntityFrameworkCore;

public static class SmartSoftwareMySqlModelBuilderExtensions
{
    public static void UseMySQL(
        this ModelBuilder modelBuilder)
    {
        modelBuilder.SetDatabaseProvider(EfCoreDatabaseProvider.MySql);
    }
}
