using System;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Localization;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

public interface ISmartSoftwareTagHelperLocalizer : ITransientDependency
{
    string GetLocalizedText(string text, ModelExplorer explorer);

    IStringLocalizer? GetLocalizerOrNull(ModelExplorer explorer);

    IStringLocalizer? GetLocalizerOrNull(Assembly assembly);
}
