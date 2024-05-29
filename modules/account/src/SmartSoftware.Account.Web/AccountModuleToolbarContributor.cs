using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Account.Web.Modules.Account.Components.Toolbar.UserLoginLink;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using SmartSoftware.Users;

namespace SmartSoftware.Account.Web;

public class AccountModuleToolbarContributor : IToolbarContributor
{
    public virtual Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name != StandardToolbars.Main)
        {
            return Task.CompletedTask;
        }

        if (!context.ServiceProvider.GetRequiredService<ICurrentUser>().IsAuthenticated)
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(UserLoginLinkViewComponent)));
        }

        return Task.CompletedTask;
    }
}
