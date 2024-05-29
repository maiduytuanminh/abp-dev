using SmartSoftware.Data;

namespace SmartSoftware.AuditLogging;

public static class SmartSoftwareAuditLoggingDbProperties
{
    public static string DbTablePrefix { get; set; } = SmartSoftwareCommonDbProperties.DbTablePrefix;

    public static string DbSchema { get; set; } = SmartSoftwareCommonDbProperties.DbSchema;

    public const string ConnectionStringName = "SmartSoftwareAuditLogging";
}
