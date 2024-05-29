using SmartSoftware.Data;

namespace SmartSoftware.IdentityServer;

public static class SmartSoftwareIdentityServerDbProperties
{
    public static string DbTablePrefix { get; set; } = "IdentityServer";

    public static string DbSchema { get; set; } = SmartSoftwareCommonDbProperties.DbSchema;

    public const string ConnectionStringName = "SmartSoftwareIdentityServer";
}
