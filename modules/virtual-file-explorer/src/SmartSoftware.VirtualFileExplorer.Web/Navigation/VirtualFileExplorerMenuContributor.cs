using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using SmartSoftware.UI.Navigation;
using SmartSoftware.VirtualFileExplorer.Web.Localization;

namespace SmartSoftware.VirtualFileExplorer.Web.Navigation;

public class VirtualFileExplorerMenuContributor : IMenuContributor
{
    public virtual Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return Task.CompletedTask;
        }

        var l = context.GetLocalizer<VirtualFileExplorerResource>();

        context.Menu.Items.Add(new ApplicationMenuItem(VirtualFileExplorerMenuNames.Index, l["Menu:VirtualFileExplorer"], icon: "fa fa-file", url: "~/VirtualFileExplorer"));

        return Task.CompletedTask;
    }
}
