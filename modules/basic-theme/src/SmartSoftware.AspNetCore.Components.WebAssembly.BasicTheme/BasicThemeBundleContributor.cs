using SmartSoftware.Bundling;

namespace SmartSoftware.AspNetCore.Components.WebAssembly.BasicTheme;

public class BasicThemeBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("_content/SmartSoftware.AspNetCore.Components.Web.BasicTheme/libs/smartsoftware/css/theme.css");
    }
}
