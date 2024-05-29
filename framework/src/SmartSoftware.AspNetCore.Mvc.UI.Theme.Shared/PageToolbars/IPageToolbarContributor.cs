using System.Threading.Tasks;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.PageToolbars;

public interface IPageToolbarContributor
{
    Task ContributeAsync(PageToolbarContributionContext context);
}
