using SmartSoftware.Data;

namespace SmartSoftware.Identity;

public static class SmartSoftwareIdentityDbProperties
{
    public static string DbTablePrefix { get; set; } = SmartSoftwareCommonDbProperties.DbTablePrefix;

    public static string DbSchema { get; set; } = SmartSoftwareCommonDbProperties.DbSchema;

    public const string ConnectionStringName = "SmartSoftwareIdentity";
}
