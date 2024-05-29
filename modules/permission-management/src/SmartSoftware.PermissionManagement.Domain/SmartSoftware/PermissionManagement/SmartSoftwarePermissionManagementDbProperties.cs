using SmartSoftware.Data;

namespace SmartSoftware.PermissionManagement;

public static class SmartSoftwarePermissionManagementDbProperties
{
    public static string DbTablePrefix { get; set; } = SmartSoftwareCommonDbProperties.DbTablePrefix;

    public static string DbSchema { get; set; } = SmartSoftwareCommonDbProperties.DbSchema;

    public const string ConnectionStringName = "SmartSoftwarePermissionManagement";
}
