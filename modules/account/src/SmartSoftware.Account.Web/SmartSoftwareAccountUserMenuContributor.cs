using System.Threading.Tasks;
using Localization.Resources.SmartSoftwareUi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using SmartSoftware.Account.Localization;
using SmartSoftware.UI.Navigation;

namespace SmartSoftware.Account.Web;

public class SmartSoftwareAccountUserMenuContributor : IMenuContributor
{
    public virtual Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.User)
        {
            return Task.CompletedTask;
        }

        var uiResource = context.GetLocalizer<SmartSoftwareUiResource>();
        var accountResource = context.GetLocalizer<AccountResource>();

        context.Menu.AddItem(new ApplicationMenuItem("Account.Manage", accountResource["MyAccount"], url: "~/Account/Manage", icon: "fa fa-cog", order: 1000, null));
        context.Menu.AddItem(new ApplicationMenuItem("Account.Logout", uiResource["Logout"], url: "~/Account/Logout", icon: "fa fa-power-off", order: int.MaxValue - 1000));

        return Task.CompletedTask;
    }
}
