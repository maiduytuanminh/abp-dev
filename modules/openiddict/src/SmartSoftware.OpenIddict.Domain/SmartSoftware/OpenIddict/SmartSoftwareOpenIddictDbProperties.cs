using SmartSoftware.Data;

namespace SmartSoftware.OpenIddict;

public static class SmartSoftwareOpenIddictDbProperties
{
    public static string DbTablePrefix { get; set; } = "OpenIddict";

    public static string DbSchema { get; set; } = SmartSoftwareCommonDbProperties.DbSchema;

    public const string ConnectionStringName = "SmartSoftwareOpenIddict";
}
