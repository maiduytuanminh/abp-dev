using SmartSoftware.Ui.Branding;
using SmartSoftware.DependencyInjection;

namespace MyCompanyName.MyProjectName;

[Dependency(ReplaceServices = true)]
public class MyProjectNameBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "MyProjectName";
}
