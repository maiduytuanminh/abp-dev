using System;

namespace SmartSoftware.AspNetCore.SignalR;

public class SmartSoftwareSignalROptions
{
    public HubConfigList Hubs { get; }

    /// <summary>
    /// Default: 5 seconds.
    /// </summary>
    public TimeSpan? CheckDynamicClaimsInterval { get; set; }

    public SmartSoftwareSignalROptions()
    {
        Hubs = new HubConfigList();
        CheckDynamicClaimsInterval = TimeSpan.FromSeconds(5);
    }
}
