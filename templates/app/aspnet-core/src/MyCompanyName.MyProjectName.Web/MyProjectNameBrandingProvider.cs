using Microsoft.Extensions.Localization;
using MyCompanyName.MyProjectName.Localization;
using SmartSoftware.Ui.Branding;
using SmartSoftware.DependencyInjection;

namespace MyCompanyName.MyProjectName.Web;

[Dependency(ReplaceServices = true)]
public class MyProjectNameBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<MyProjectNameResource> _localizer;

    public MyProjectNameBrandingProvider(IStringLocalizer<MyProjectNameResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
