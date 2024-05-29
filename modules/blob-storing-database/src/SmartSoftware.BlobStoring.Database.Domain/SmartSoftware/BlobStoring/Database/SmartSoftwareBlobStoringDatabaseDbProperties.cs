using SmartSoftware.Data;

namespace SmartSoftware.BlobStoring.Database;

public static class SmartSoftwareBlobStoringDatabaseDbProperties
{
    public static string DbTablePrefix { get; set; } = SmartSoftwareCommonDbProperties.DbTablePrefix;

    public static string DbSchema { get; set; } = SmartSoftwareCommonDbProperties.DbSchema;

    public const string ConnectionStringName = "SmartSoftwareBlobStoring";
}
