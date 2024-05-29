using SmartSoftware.Ui.Branding;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.Demo;

public class BootstrapDemoBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Bootstrap Tag Helpers";

    public override string LogoUrl => "/imgs/demo/ss-io-light.png";
}