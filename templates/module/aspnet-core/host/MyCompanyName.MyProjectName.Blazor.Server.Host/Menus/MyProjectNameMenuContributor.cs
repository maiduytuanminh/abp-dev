﻿using System.Threading.Tasks;
using MyCompanyName.MyProjectName.MultiTenancy;
using SmartSoftware.Identity.Blazor;
using SmartSoftware.SettingManagement.Blazor.Menus;
using SmartSoftware.TenantManagement.Blazor.Navigation;
using SmartSoftware.UI.Navigation;

namespace MyCompanyName.MyProjectName.Blazor.Server.Host.Menus;

public class MyProjectNameMenuContributor : IMenuContributor
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
        var administration = context.Menu.GetAdministration();

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);

        return Task.CompletedTask;
    }
}
