using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;
using SmartSoftware.VirtualFileExplorer.Web.Localization;

namespace SmartSoftware.VirtualFileExplorer.Web.Pages.VirtualFileExplorer;

public abstract class VirtualFileExplorerPageModel : SmartSoftwarePageModel
{
    protected VirtualFileExplorerPageModel()
    {
        LocalizationResourceType = typeof(VirtualFileExplorerResource);
        ObjectMapperContext = typeof(SmartSoftwareVirtualFileExplorerWebModule);
    }
}
