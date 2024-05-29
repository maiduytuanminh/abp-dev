using System.Threading.Tasks;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;

public interface IToolbarContributor
{
    Task ConfigureToolbarAsync(IToolbarConfigurationContext context);
}
