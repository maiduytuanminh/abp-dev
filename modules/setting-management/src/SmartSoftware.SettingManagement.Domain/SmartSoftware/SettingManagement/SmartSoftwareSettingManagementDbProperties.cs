using SmartSoftware.Data;

namespace SmartSoftware.SettingManagement;

public static class SmartSoftwareSettingManagementDbProperties
{
    public static string DbTablePrefix { get; set; } = SmartSoftwareCommonDbProperties.DbTablePrefix;

    public static string DbSchema { get; set; } = SmartSoftwareCommonDbProperties.DbSchema;

    public const string ConnectionStringName = "SmartSoftwareSettingManagement";
}
