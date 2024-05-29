using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.UI;
using SmartSoftware.AspNetCore.Mvc.UI.Widgets;
using SmartSoftware.CmsKit.Public.Reactions;
using SmartSoftware.CmsKit.Web;

namespace SmartSoftware.CmsKit.Public.Web.Pages.CmsKit.Shared.Components.ReactionSelection;

[ViewComponent(Name = "CmsReactionSelection")]
[Widget(
    ScriptTypes = new[] { typeof(ReactionSelectionScriptBundleContributor) },
    StyleTypes = new[] { typeof(ReactionSelectionStyleBundleContributor) },
    RefreshUrl = "/CmsKitPublicWidgets/ReactionSelection",
    AutoInitialize = true
)]
public class ReactionSelectionViewComponent : SmartSoftwareViewComponent
{
    protected IReactionPublicAppService ReactionPublicAppService { get; }

    protected CmsKitUiOptions Options { get; }

    public SmartSoftwareMvcUiOptions SmartSoftwareMvcUiOptions { get; }

    public ReactionSelectionViewComponent(
        IReactionPublicAppService reactionPublicAppService,
        IOptions<CmsKitUiOptions> options,
        IOptions<SmartSoftwareMvcUiOptions> ssMvcUiOptions)
    {
        ReactionPublicAppService = reactionPublicAppService;
        Options = options.Value;
        SmartSoftwareMvcUiOptions = ssMvcUiOptions.Value;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync(
        string entityType,
        string entityId)
    {
        var result = await ReactionPublicAppService.GetForSelectionAsync(entityType, entityId);

        var loginUrl =
            $"{SmartSoftwareMvcUiOptions.LoginUrl}?returnUrl={HttpContext.Request.Path.ToString()}&returnUrlHash=#cms-rating_{entityType}_{entityId}";

        var viewModel = new ReactionSelectionViewModel
        {
            EntityType = entityType,
            EntityId = entityId,
            Reactions = new List<ReactionViewModel>(),
            LoginUrl = loginUrl
        };

        foreach (var reactionDto in result.Items)
        {
            viewModel.Reactions.Add(
                new ReactionViewModel //TODO: AutoMap
                    {
                    Name = reactionDto.Reaction.Name,
                    DisplayName = reactionDto.Reaction.DisplayName,
                    Icon = Options.ReactionIcons.GetLocalizedIcon(reactionDto.Reaction.Name),
                    Count = reactionDto.Count,
                    IsSelectedByCurrentUser = reactionDto.IsSelectedByCurrentUser
                });
        }

        return View("~/Pages/CmsKit/Shared/Components/ReactionSelection/Default.cshtml", viewModel);
    }

    public class ReactionSelectionViewModel
    {
        public string EntityType { get; set; }

        public string EntityId { get; set; }

        public List<ReactionViewModel> Reactions { get; set; }

        public string LoginUrl { get; set; }
    }

    public class ReactionViewModel
    {
        [NotNull]
        public string Name { get; set; }

        [CanBeNull]
        public string DisplayName { get; set; }

        [NotNull]
        public string Icon { get; set; }

        public int Count { get; set; }

        public bool IsSelectedByCurrentUser { get; set; }
    }
}
