using SmartSoftware.Data;

namespace SmartSoftware.CmsKit;

public static class SmartSoftwareCmsKitDbProperties
{
    public static string DbTablePrefix { get; set; } = "Cms";

    public static string DbSchema { get; set; } = SmartSoftwareCommonDbProperties.DbSchema;

    public const string ConnectionStringName = "CmsKit";
}
