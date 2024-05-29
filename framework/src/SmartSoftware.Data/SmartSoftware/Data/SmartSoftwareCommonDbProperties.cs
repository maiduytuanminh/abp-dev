namespace SmartSoftware.Data;

public static class SmartSoftwareCommonDbProperties
{
    /// <summary>
    /// This table prefix is shared by most of the SS modules.
    /// You can change it to set table prefix for all modules using this.
    /// 
    /// Default value: "SmartSoftware".
    /// </summary>
    public static string DbTablePrefix { get; set; } = "SmartSoftware";

    /// <summary>
    /// Default value: null.
    /// </summary>
    public static string? DbSchema { get; set; } = null;
}
