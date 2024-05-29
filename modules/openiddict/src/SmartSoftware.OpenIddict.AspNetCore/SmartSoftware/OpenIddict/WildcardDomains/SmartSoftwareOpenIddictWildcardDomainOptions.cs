using System.Collections.Generic;

namespace SmartSoftware.OpenIddict.WildcardDomains;

public class SmartSoftwareOpenIddictWildcardDomainOptions
{
    public bool EnableWildcardDomainSupport { get; set; }

    public HashSet<string> WildcardDomainsFormat { get; }

    public SmartSoftwareOpenIddictWildcardDomainOptions()
    {
        WildcardDomainsFormat = new HashSet<string>();
    }
}
