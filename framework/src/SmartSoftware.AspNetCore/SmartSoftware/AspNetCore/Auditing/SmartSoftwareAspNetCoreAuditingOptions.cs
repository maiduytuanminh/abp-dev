using System.Collections.Generic;

namespace SmartSoftware.AspNetCore.Auditing;

public class SmartSoftwareAspNetCoreAuditingOptions
{
    /// <summary>
    /// This is used to disable the <see cref="SmartSoftwareAuditingMiddleware"/>,
    /// app.UseAuditing(), for the specified URLs.
    /// <see cref="SmartSoftwareAuditingMiddleware"/> will be disabled for URLs
    /// starting with an ignored URL.  
    /// </summary>
    public List<string> IgnoredUrls { get; } = new();
}
