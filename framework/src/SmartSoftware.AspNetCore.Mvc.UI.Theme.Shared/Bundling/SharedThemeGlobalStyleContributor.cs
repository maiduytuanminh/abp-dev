using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Bootstrap;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.BootstrapDatepicker;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.BootstrapDaterangepicker;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Core;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.DatatablesNetBs5;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.FontAwesome;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.MalihuCustomScrollbar;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Select2;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Toastr;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Bundling;

[DependsOn(
    typeof(CoreStyleContributor),
    typeof(BootstrapStyleContributor),
    typeof(FontAwesomeStyleContributor),
    typeof(ToastrStyleBundleContributor),
    typeof(Select2StyleContributor),
    typeof(MalihuCustomScrollbarPluginStyleBundleContributor),
    typeof(DatatablesNetBs5StyleContributor),
    typeof(BootstrapDatepickerStyleContributor),
    typeof(BootstrapDaterangepickerStyleContributor)
)]
public class SharedThemeGlobalStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddRange(new BundleFile[]
        {
            "/libs/smartsoftware/aspnetcore-mvc-ui-theme-shared/datatables/datatables-styles.css",
            "/libs/smartsoftware/aspnetcore-mvc-ui-theme-shared/date-range-picker/date-range-picker-styles.css"
        });
    }
}
