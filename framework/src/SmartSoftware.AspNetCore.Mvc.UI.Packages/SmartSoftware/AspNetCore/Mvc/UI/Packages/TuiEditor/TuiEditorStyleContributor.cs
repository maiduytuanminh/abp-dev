using System.Collections.Generic;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Packages.Prismjs;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Mvc.UI.Packages.TuiEditor;

[DependsOn(
    typeof(PrismjsStyleBundleContributor)
)]
public class TuiEditorStyleContributor : BundleContributor
{
    public override void ConfigureBundle(BundleConfigurationContext context)
    {
        context.Files.AddIfNotContains("/libs/tui-editor/toastui-editor.min.css");
        context.Files.AddIfNotContains("/libs/tui-editor/toastui-editor-plugin-code-syntax-highlight.min.css");
    }
}
