using OpenIddict.Abstractions;

namespace SmartSoftware.OpenIddict.Applications;

public class SmartSoftwareApplicationDescriptor : OpenIddictApplicationDescriptor
{
    /// <summary>
    /// URI to further information about client.
    /// </summary>
    public string ClientUri { get; set; }

    /// <summary>
    /// URI to client logo.
    /// </summary>
    public string LogoUri { get; set; }
}
