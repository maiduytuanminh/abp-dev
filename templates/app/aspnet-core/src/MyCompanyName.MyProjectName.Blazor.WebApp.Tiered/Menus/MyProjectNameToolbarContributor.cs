﻿using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MyCompanyName.MyProjectName.Blazor.WebApp.Tiered.Components.Toolbar.LoginLink;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using SmartSoftware.Users;

namespace MyCompanyName.MyProjectName.Blazor.WebApp.Tiered.Menus;

public class MyProjectNameToolbarContributor : IToolbarContributor
{
    public virtual Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
    {
        if (context.Toolbar.Name != StandardToolbars.Main)
        {
            return Task.CompletedTask;
        }

        if (!context.ServiceProvider.GetRequiredService<ICurrentUser>().IsAuthenticated)
        {
            context.Toolbar.Items.Add(new ToolbarItem(typeof(LoginLinkViewComponent)));
        }

        return Task.CompletedTask;
    }
}
