using System.Collections.Generic;

namespace SmartSoftware.Json;

public class SmartSoftwareJsonOptions
{
    /// <summary>
    /// Formats of input JSON date, Empty string means default format.
    /// </summary>
    public List<string> InputDateTimeFormats { get; set; }

    /// <summary>
    /// Format of output json date, Null or empty string means default format.
    /// </summary>
    public string? OutputDateTimeFormat { get; set; }

    public SmartSoftwareJsonOptions()
    {
        InputDateTimeFormats = new List<string>();
    }
}
