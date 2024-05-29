using System.Threading.Tasks;
using SmartSoftware.TenantManagement.Localization;
using SmartSoftware.UI.Navigation;
using SmartSoftware.Authorization.Permissions;

namespace SmartSoftware.TenantManagement.Blazor.Navigation;

public class TenantManagementBlazorMenuContributor : IMenuContributor
{
    public virtual Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return Task.CompletedTask;
        }

        var administrationMenu = context.Menu.GetAdministration();

        var l = context.GetLocalizer<SmartSoftwareTenantManagementResource>();

        var tenantManagementMenuItem = new ApplicationMenuItem(
            TenantManagementMenuNames.GroupName,
            l["Menu:TenantManagement"],
            icon: "fa fa-users"
        );
        administrationMenu.AddItem(tenantManagementMenuItem);

        tenantManagementMenuItem.AddItem(new ApplicationMenuItem(
            TenantManagementMenuNames.Tenants,
            l["Tenants"],
            url: "~/tenant-management/tenants"
        ).RequirePermissions(TenantManagementPermissions.Tenants.Default));

        return Task.CompletedTask;
    }
}
