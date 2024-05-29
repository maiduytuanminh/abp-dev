using SmartSoftware.Security.Claims;

namespace SmartSoftware.OpenIddict;

public class SmartSoftwareOpenIddictAspNetCoreOptions
{
    /// <summary>
    /// Updates <see cref="SmartSoftwareClaimTypes"/> to be compatible with OpenIddict claims.
    /// Default: true.
    /// </summary>
    public bool UpdateSmartSoftwareClaimTypes { get; set; } = true;

    /// <summary>
    /// Set false to suppress AddDeveloperSigningCredential() call on the OpenIddictBuilder.
    /// Default: true.
    /// </summary>
    public bool AddDevelopmentEncryptionAndSigningCertificate { get; set; } = true;
}
