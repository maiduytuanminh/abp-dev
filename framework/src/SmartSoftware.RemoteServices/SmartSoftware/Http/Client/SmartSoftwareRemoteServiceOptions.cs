namespace SmartSoftware.Http.Client;

public class SmartSoftwareRemoteServiceOptions
{
    public RemoteServiceConfigurationDictionary RemoteServices { get; set; }

    public SmartSoftwareRemoteServiceOptions()
    {
        RemoteServices = new RemoteServiceConfigurationDictionary();
    }
}
