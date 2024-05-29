using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Mvc.Localization;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

public class SmartSoftwareTagHelperLocalizer : ISmartSoftwareTagHelperLocalizer
{
    private readonly IStringLocalizerFactory _stringLocalizerFactory;
    private readonly SmartSoftwareMvcDataAnnotationsLocalizationOptions _options;

    public SmartSoftwareTagHelperLocalizer(IOptions<SmartSoftwareMvcDataAnnotationsLocalizationOptions> options, IStringLocalizerFactory stringLocalizerFactory)
    {
        _stringLocalizerFactory = stringLocalizerFactory;
        _options = options.Value;
    }

    public string GetLocalizedText(string text, ModelExplorer explorer)
    {
        var localizer = GetLocalizerOrNull(explorer);
        return localizer == null
            ? text
            : localizer[text].Value;
    }

    public IStringLocalizer? GetLocalizerOrNull(ModelExplorer explorer)
    {
        return GetLocalizerOrNull(explorer.Container.ModelType.Assembly);
    }

    public IStringLocalizer? GetLocalizerOrNull(Assembly assembly)
    {
        var resourceType = GetResourceType(assembly);
        return resourceType == null
            ? _stringLocalizerFactory.CreateDefaultOrNull()
            : _stringLocalizerFactory.Create(resourceType);
    }

    private Type? GetResourceType(Assembly assembly)
    {
        return _options
            .AssemblyResources
            .GetOrDefault(assembly);
    }
}
