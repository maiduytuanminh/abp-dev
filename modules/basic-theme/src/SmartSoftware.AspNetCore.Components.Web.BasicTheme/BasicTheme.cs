using System;
using SmartSoftware.AspNetCore.Components.Web.BasicTheme.Themes.Basic;
using SmartSoftware.AspNetCore.Components.Web.Theming.Layout;
using SmartSoftware.AspNetCore.Components.Web.Theming.Theming;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Components.Web.BasicTheme;

[ThemeName(Name)]
public class BasicTheme : ITheme, ITransientDependency
{
    public const string Name = "Basic";

    public virtual Type GetLayout(string name, bool fallbackToDefault = true)
    {
        switch (name)
        {
            case StandardLayouts.Application:
            case StandardLayouts.Account:
            case StandardLayouts.Empty:
                return typeof(MainLayout);
            default:
                return fallbackToDefault ? typeof(MainLayout) : typeof(NullLayout);
        }
    }
}