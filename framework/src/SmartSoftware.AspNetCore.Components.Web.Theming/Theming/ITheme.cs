using System;

namespace SmartSoftware.AspNetCore.Components.Web.Theming.Theming;

public interface ITheme
{
    Type GetLayout(string name, bool fallbackToDefault = true);
}
