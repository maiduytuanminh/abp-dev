namespace SmartSoftware.Identity;

public class SmartSoftwareIdentityOptions
{
    public ExternalLoginProviderDictionary ExternalLoginProviders { get; }

    public SmartSoftwareIdentityOptions()
    {
        ExternalLoginProviders = new ExternalLoginProviderDictionary();
    }
}
