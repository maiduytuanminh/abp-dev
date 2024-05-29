using System.Collections.Generic;

namespace SmartSoftware.AspNetCore.Uow;

public class SmartSoftwareAspNetCoreUnitOfWorkOptions
{
    /// <summary>
    /// This is used to disable the <see cref="SmartSoftwareUnitOfWorkMiddleware"/>,
    /// app.UseUnitOfWork(), for the specified URLs.
    /// <see cref="SmartSoftwareUnitOfWorkMiddleware"/> will be disabled for URLs
    /// starting with an ignored URL.  
    /// </summary>
    public List<string> IgnoredUrls { get; } = new List<string>();
}
