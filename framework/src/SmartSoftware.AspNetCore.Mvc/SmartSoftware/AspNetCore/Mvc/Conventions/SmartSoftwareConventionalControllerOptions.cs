using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using SmartSoftware.Content;
using SmartSoftware.Http.Modeling;

namespace SmartSoftware.AspNetCore.Mvc.Conventions;

public class SmartSoftwareConventionalControllerOptions
{
    public ConventionalControllerSettingList ConventionalControllerSettings { get; }

    public List<Type> FormBodyBindingIgnoredTypes { get; }

    /// <summary>
    /// Set true to use the old style URL path style.
    /// Default: false.
    /// </summary>
    public bool UseV3UrlStyle { get; set; }

    public string[] IgnoredUrlSuffixesInControllerNames { get; set; } = new[] { "Integration" };

    public SmartSoftwareConventionalControllerOptions()
    {
        ConventionalControllerSettings = new ConventionalControllerSettingList();

        FormBodyBindingIgnoredTypes = new List<Type>
            {
                typeof(IFormFile),
                typeof(IRemoteStreamContent)
            };
    }

    public SmartSoftwareConventionalControllerOptions Create(
        Assembly assembly,
        Action<ConventionalControllerSetting>? optionsAction = null)
    {
        var setting = new ConventionalControllerSetting(
            assembly,
            ModuleApiDescriptionModel.DefaultRootPath,
            ModuleApiDescriptionModel.DefaultRemoteServiceName
        );

        optionsAction?.Invoke(setting);
        setting.Initialize();
        ConventionalControllerSettings.Add(setting);
        return this;
    }
}
