using SmartSoftware.Ui.Branding;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.CmsKit;

[Dependency(ReplaceServices = true)]
public class CmsKitBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "CmsKit";
}
