﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Components.Web.Theming.Toolbars;

public class ToolbarManager : IToolbarManager, ITransientDependency
{
    protected SmartSoftwareToolbarOptions Options { get; }
    protected IServiceProvider ServiceProvider { get; }

    public ToolbarManager(
        IOptions<SmartSoftwareToolbarOptions> options,
        IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        Options = options.Value;
    }

    public async Task<Toolbar> GetAsync(string name)
    {
        var toolbar = new Toolbar(name);

        using (var scope = ServiceProvider.CreateScope())
        {
            var context = new ToolbarConfigurationContext(toolbar, scope.ServiceProvider);

            foreach (var contributor in Options.Contributors)
            {
                await contributor.ConfigureToolbarAsync(context);
            }
        }

        return toolbar;
    }
}
