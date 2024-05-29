using SmartSoftware.DependencyInjection;
using SmartSoftware.Ui.Branding;

namespace MyCompanyName.MyProjectName.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class MyProjectNameBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "MyProjectName";
}
