using System.Threading.Tasks;

namespace SmartSoftware.AspNetCore.Components.Web.Theming.Toolbars;

public interface IToolbarContributor
{
    Task ConfigureToolbarAsync(IToolbarConfigurationContext context);
}
