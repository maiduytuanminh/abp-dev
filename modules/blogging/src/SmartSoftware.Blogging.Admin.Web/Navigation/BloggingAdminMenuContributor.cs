using System.Threading.Tasks;
using SmartSoftware.UI.Navigation;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.Blogging.Localization;

namespace SmartSoftware.Blogging.Admin
{
    public class BloggingAdminMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<BloggingResource>();

            var managementRootMenuItem = new ApplicationMenuItem(BloggingAdminMenuNames.GroupName, l["Menu:BlogManagement"]).RequirePermissions(BloggingPermissions.Blogs.Management);

            managementRootMenuItem.AddItem(new ApplicationMenuItem(BloggingAdminMenuNames.Blogs, l["Menu:Blogs"], "~/Blogging/Admin/Blogs").RequirePermissions(BloggingPermissions.Blogs.Management));

            context.Menu.GetAdministration().AddItem(managementRootMenuItem);

            return Task.CompletedTask;
        }
    }
}
