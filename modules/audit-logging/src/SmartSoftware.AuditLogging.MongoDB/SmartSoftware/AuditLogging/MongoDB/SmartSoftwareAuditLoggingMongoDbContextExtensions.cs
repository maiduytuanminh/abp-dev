using SmartSoftware.MongoDB;

namespace SmartSoftware.AuditLogging.MongoDB;

public static class SmartSoftwareAuditLoggingMongoDbContextExtensions
{
    public static void ConfigureAuditLogging(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<AuditLog>(b =>
        {
            b.CollectionName = SmartSoftwareAuditLoggingDbProperties.DbTablePrefix + "AuditLogs";
        });
    }
}
