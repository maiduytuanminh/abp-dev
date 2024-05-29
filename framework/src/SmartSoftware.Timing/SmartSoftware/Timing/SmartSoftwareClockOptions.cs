using System;

namespace SmartSoftware.Timing;

public class SmartSoftwareClockOptions
{
    /// <summary>
    /// Default: <see cref="DateTimeKind.Unspecified"/>
    /// </summary>
    public DateTimeKind Kind { get; set; }

    public SmartSoftwareClockOptions()
    {
        Kind = DateTimeKind.Unspecified;
    }
}
