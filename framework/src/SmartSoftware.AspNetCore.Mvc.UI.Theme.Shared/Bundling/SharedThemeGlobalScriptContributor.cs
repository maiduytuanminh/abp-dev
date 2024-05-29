using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Bootstrap;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.BootstrapDatepicker;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.BootstrapDaterangepicker;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.DatatablesNetBs5;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.JQuery;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.JQueryForm;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.JQueryValidationUnobtrusive;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Lodash;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Luxon;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.MalihuCustomScrollbar;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Select2;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.SweetAlert2;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Timeago;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Toastr;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Bundling;

[DependsOn(
    typeof(JQueryScriptContributor),
    typeof(BootstrapScriptContributor),
    typeof(LodashScriptContributor),
    typeof(JQueryValidationUnobtrusiveScriptContributor),
    typeof(JQueryFormScriptContributor),
    typeof(Select2ScriptContributor),
    typeof(DatatablesNetBs5ScriptContributor),
    typeof(Sweetalert2ScriptContributor),
    typeof(ToastrScriptBundleContributor),
    typeof(MalihuCustomScrollbarPluginScriptBundleContributor),
    typeof(LuxonScriptContributor),
    typeof(TimeagoScriptContributor),
    typeof(BootstrapDatepickerScriptContributor),
    typeof(BootstrapDaterangepickerScriptContributor)
    )]
public class SharedThemeGlobalScriptContributor : BundleContributor
{

    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddRange(new BundleFile[]
        {
            "/libs/smartsoftware/aspnetcore-mvc-ui-theme-shared/ui-extensions.js",
            "/libs/smartsoftware/aspnetcore-mvc-ui-theme-shared/jquery/jquery-extensions.js",
            "/libs/smartsoftware/aspnetcore-mvc-ui-theme-shared/jquery-form/jquery-form-extensions.js",
            "/libs/smartsoftware/aspnetcore-mvc-ui-theme-shared/jquery/widget-manager.js",
            "/libs/smartsoftware/aspnetcore-mvc-ui-theme-shared/bootstrap/dom-event-handlers.js",
            "/libs/smartsoftware/aspnetcore-mvc-ui-theme-shared/bootstrap/modal-manager.js",
            "/libs/smartsoftware/aspnetcore-mvc-ui-theme-shared/datatables/datatables-extensions.js",
            "/libs/smartsoftware/aspnetcore-mvc-ui-theme-shared/sweetalert2/ss-sweetalert2.js",
            "/libs/smartsoftware/aspnetcore-mvc-ui-theme-shared/toastr/ss-toastr.js",
            "/libs/smartsoftware/aspnetcore-mvc-ui-theme-shared/date-range-picker/date-range-picker-extensions.js",
            "/libs/smartsoftware/aspnetcore-mvc-ui-theme-shared/authentication-state/authentication-state-listener.js"
        });
    }
}
