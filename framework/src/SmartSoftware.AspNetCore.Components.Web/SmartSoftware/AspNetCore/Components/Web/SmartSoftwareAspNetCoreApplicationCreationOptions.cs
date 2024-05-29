namespace SmartSoftware.AspNetCore.Components.Web;

public class SmartSoftwareAspNetCoreApplicationCreationOptions
{
    public SmartSoftwareApplicationCreationOptions ApplicationCreationOptions { get; }

    public SmartSoftwareAspNetCoreApplicationCreationOptions(
        SmartSoftwareApplicationCreationOptions applicationCreationOptions)
    {
        ApplicationCreationOptions = applicationCreationOptions;
    }
}
