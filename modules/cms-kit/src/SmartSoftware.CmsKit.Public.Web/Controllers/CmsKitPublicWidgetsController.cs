using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.CmsKit.Public.Web.Pages.CmsKit.Shared.Components.Commenting;
using SmartSoftware.CmsKit.Public.Web.Pages.CmsKit.Shared.Components.Rating;
using SmartSoftware.CmsKit.Public.Web.Pages.CmsKit.Shared.Components.ReactionSelection;

namespace SmartSoftware.CmsKit.Public.Web.Controllers;

public class CmsKitPublicWidgetsController : CmsKitPublicControllerBase
{
    public Task<IActionResult> ReactionSelection(string entityType, string entityId)
    {
        return Task.FromResult((IActionResult)ViewComponent(typeof(ReactionSelectionViewComponent), new { entityType, entityId }));
    }

    public Task<IActionResult> Commenting(string entityType, string entityId)
    {
        return Task.FromResult((IActionResult)ViewComponent(typeof(CommentingViewComponent), new { entityType, entityId }));
    }

    public Task<IActionResult> Rating(string entityType, string entityId)
    {
        return Task.FromResult((IActionResult)ViewComponent(typeof(RatingViewComponent), new { entityType, entityId }));
    }
}
