using System.Threading.Tasks;

namespace SmartSoftware.AspNetCore.Components.Web.Theming.PageToolbars;

public interface IPageToolbarContributor
{
    Task ContributeAsync(PageToolbarContributionContext context);
}
