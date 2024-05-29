using SmartSoftware.Data;

namespace SmartSoftware.TenantManagement;

public static class SmartSoftwareTenantManagementDbProperties
{
    public static string DbTablePrefix { get; set; } = SmartSoftwareCommonDbProperties.DbTablePrefix;

    public static string DbSchema { get; set; } = SmartSoftwareCommonDbProperties.DbSchema;

    public const string ConnectionStringName = "SmartSoftwareTenantManagement";
}
