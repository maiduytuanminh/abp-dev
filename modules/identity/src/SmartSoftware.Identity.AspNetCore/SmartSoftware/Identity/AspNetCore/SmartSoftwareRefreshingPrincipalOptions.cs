using System.Collections.Generic;
using SmartSoftware.Security.Claims;

namespace SmartSoftware.Identity.AspNetCore;

public class SmartSoftwareRefreshingPrincipalOptions
{
    public List<string> CurrentPrincipalKeepClaimTypes { get; set; }

    public SmartSoftwareRefreshingPrincipalOptions()
    {
        CurrentPrincipalKeepClaimTypes = new List<string>
        {
            SmartSoftwareClaimTypes.SessionId
        };
    }
}
