using System.Collections.Generic;

namespace SmartSoftware.IdentityServer;

public class SmartSoftwareClaimsServiceOptions
{
    public List<string> RequestedClaims { get; }

    public SmartSoftwareClaimsServiceOptions()
    {
        RequestedClaims = new List<string>();
    }
}
