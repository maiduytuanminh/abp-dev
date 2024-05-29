﻿using Microsoft.Extensions.Localization;
using MyCompanyName.MyProjectName.Localization;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Ui.Branding;

namespace MyCompanyName.MyProjectName.Blazor.WebApp.Client;

[Dependency(ReplaceServices = true)]
public class MyProjectNameBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<MyProjectNameResource> _localizer;

    public MyProjectNameBrandingProvider(IStringLocalizer<MyProjectNameResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
