using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.UI;
using SmartSoftware.AspNetCore.Mvc.UI.Widgets;
using SmartSoftware.Users;
using SmartSoftware.CmsKit.Public.Ratings;

namespace SmartSoftware.CmsKit.Public.Web.Pages.CmsKit.Shared.Components.Rating;

[ViewComponent(Name = "CmsRating")]
[Widget(
    StyleTypes = new[] { typeof(RatingStyleBundleContributor) },
    ScriptTypes = new[] { typeof(RatingScriptBundleContributor) },
    RefreshUrl = "/CmsKitPublicWidgets/Rating",
    AutoInitialize = true
)]
public class RatingViewComponent : SmartSoftwareViewComponent
{
    public IRatingPublicAppService RatingPublicAppService { get; }
    public SmartSoftwareMvcUiOptions SmartSoftwareMvcUiOptions { get; }
    public ICurrentUser CurrentUser { get; }

    public RatingViewComponent(IRatingPublicAppService ratingPublicAppService, IOptions<SmartSoftwareMvcUiOptions> options, ICurrentUser currentUser)
    {
        RatingPublicAppService = ratingPublicAppService;
        SmartSoftwareMvcUiOptions = options.Value;
        CurrentUser = currentUser;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync(string entityType, string entityId, bool isReadOnly = false)
    {
        var ratings = await RatingPublicAppService.GetGroupedStarCountsAsync(entityType, entityId);
        var totalRating = ratings.Sum(x => x.Count);

        short? currentUserRating = null;
        if (CurrentUser.IsAuthenticated)
        {
            currentUserRating = ratings.Find(x => x.IsSelectedByCurrentUser)?.StarCount;
        }

        var loginUrl =
            $"{SmartSoftwareMvcUiOptions.LoginUrl}?returnUrl={HttpContext.Request.Path.ToString()}&returnUrlHash=#cms-rating_{entityType}_{entityId}";

        var viewModel = new RatingViewModel
        {
            EntityId = entityId,
            EntityType = entityType,
            LoginUrl = loginUrl,
            Ratings = ratings,
            CurrentRating = currentUserRating,
            TotalRating = totalRating,
            IsReadOnly = isReadOnly
        };

        return View("~/Pages/CmsKit/Shared/Components/Rating/Default.cshtml", viewModel);
    }
}

public class RatingViewModel
{
    public string EntityType { get; set; }

    public string EntityId { get; set; }

    public string LoginUrl { get; set; }

    public List<RatingWithStarCountDto> Ratings { get; set; }

    public short? CurrentRating { get; set; }

    public int TotalRating { get; set; }

    public bool IsReadOnly { get; set; }
}
