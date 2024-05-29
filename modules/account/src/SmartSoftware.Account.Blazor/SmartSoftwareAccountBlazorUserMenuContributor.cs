using System.Threading.Tasks;
using SmartSoftware.Account.Localization;
using SmartSoftware.UI.Navigation;

namespace SmartSoftware.Account.Blazor;

public class SmartSoftwareAccountBlazorUserMenuContributor : IMenuContributor
{
    public Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.User)
        {
            return Task.CompletedTask;
        }

        var accountResource = context.GetLocalizer<AccountResource>();

        context.Menu.AddItem(new ApplicationMenuItem("Account.Manage", accountResource["MyAccount"], url: "account/manage-profile", icon: "fa fa-cog"));

        return Task.CompletedTask;
    }
}
