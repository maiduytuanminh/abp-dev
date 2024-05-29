using SmartSoftware.Data;

namespace SmartSoftware.Docs
{
    public static class SmartSoftwareDocsDbProperties
    {
        public static string DbTablePrefix { get; set; } = "Docs";

        public static string DbSchema { get; set; } = SmartSoftwareCommonDbProperties.DbSchema;

        public const string ConnectionStringName = "Docs";
    }
}
