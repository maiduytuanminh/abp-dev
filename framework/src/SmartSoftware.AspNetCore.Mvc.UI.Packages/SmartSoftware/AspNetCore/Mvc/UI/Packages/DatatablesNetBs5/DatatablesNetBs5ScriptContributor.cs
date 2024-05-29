using System.Collections.Generic;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Bootstrap;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.DatatablesNet;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.DatatablesNetBs5;

[DependsOn(typeof(DatatablesNetScriptContributor))]
[DependsOn(typeof(BootstrapScriptContributor))]
public class DatatablesNetBs5ScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/libs/datatables.net-bs5/js/dataTables.bootstrap5.js");
    }
}
