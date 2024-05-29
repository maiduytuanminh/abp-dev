using SmartSoftware.Data;

namespace SmartSoftware.FeatureManagement;

public static class SmartSoftwareFeatureManagementDbProperties
{
    public static string DbTablePrefix { get; set; } = SmartSoftwareCommonDbProperties.DbTablePrefix;

    public static string DbSchema { get; set; } = SmartSoftwareCommonDbProperties.DbSchema;

    public const string ConnectionStringName = "SmartSoftwareFeatureManagement";
}
