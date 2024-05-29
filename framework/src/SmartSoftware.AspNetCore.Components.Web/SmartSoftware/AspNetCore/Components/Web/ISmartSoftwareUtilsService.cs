using System.Threading.Tasks;

namespace SmartSoftware.AspNetCore.Components.Web;

public interface ISmartSoftwareUtilsService
{
    ValueTask AddClassToTagAsync(string tagName, string className);

    ValueTask RemoveClassFromTagAsync(string tagName, string className);

    ValueTask<bool> HasClassOnTagAsync(string tagName, string className);

    ValueTask ReplaceLinkHrefByIdAsync(string linkId, string hrefValue);

    ValueTask ToggleFullscreenAsync();

    ValueTask RequestFullscreenAsync();

    ValueTask ExitFullscreenAsync();
}
