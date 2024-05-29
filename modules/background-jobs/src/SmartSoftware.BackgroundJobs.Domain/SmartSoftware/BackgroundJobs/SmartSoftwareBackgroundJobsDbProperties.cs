using SmartSoftware.Data;

namespace SmartSoftware.BackgroundJobs;

public static class SmartSoftwareBackgroundJobsDbProperties
{
    public static string DbTablePrefix { get; set; } = SmartSoftwareCommonDbProperties.DbTablePrefix;

    public static string DbSchema { get; set; } = SmartSoftwareCommonDbProperties.DbSchema;

    public const string ConnectionStringName = "SmartSoftwareBackgroundJobs";
}
