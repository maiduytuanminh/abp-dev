using System.Collections.Generic;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Bootstrap;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.DatatablesNet;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.DatatablesNetBs4;

[DependsOn(typeof(DatatablesNetScriptContributor))]
[DependsOn(typeof(BootstrapScriptContributor))]
public class DatatablesNetBs4ScriptContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/libs/datatables.net-bs4/js/dataTables.bootstrap4.js");
    }
}
