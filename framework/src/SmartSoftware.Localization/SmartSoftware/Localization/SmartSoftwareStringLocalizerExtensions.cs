﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Localization;
using SmartSoftware.DynamicProxy;
using SmartSoftware.Reflection;

namespace SmartSoftware.Localization;

public static class SmartSoftwareStringLocalizerExtensions
{
    [NotNull]
    public static IStringLocalizer GetInternalLocalizer(
        [NotNull] this IStringLocalizer stringLocalizer)
    {
        Check.NotNull(stringLocalizer, nameof(stringLocalizer));

        var localizerType = stringLocalizer.GetType();
        if (!ReflectionHelper.IsAssignableToGenericType(localizerType, typeof(StringLocalizer<>)))
        {
            return stringLocalizer;
        }

        var localizerField = localizerType
            .GetField(
                "_localizer",
                BindingFlags.Instance |
                BindingFlags.NonPublic
            );

        if (localizerField == null)
        {
            throw new SmartSoftwareException($"Could not find the _localizer field inside the {typeof(StringLocalizer<>).FullName} class. Probably its name has changed. Please report this issue to the SS framework.");
        }

        return (localizerField.GetValue(stringLocalizer) as IStringLocalizer)!;
    }

    public static IEnumerable<LocalizedString> GetAllStrings(
        this IStringLocalizer stringLocalizer,
        bool includeParentCultures,
        bool includeBaseLocalizers,
        bool includeDynamicContributors)
    {
        var internalLocalizer = ((IStringLocalizer)ProxyHelper.UnProxy(stringLocalizer)).GetInternalLocalizer();
        if (internalLocalizer is ISmartSoftwareStringLocalizer ssStringLocalizer)
        {
            return ssStringLocalizer.GetAllStrings(
                includeParentCultures,
                includeBaseLocalizers,
                includeDynamicContributors
            );
        }

        return stringLocalizer.GetAllStrings(
            includeParentCultures
        );
    }
    
    public static async Task<IEnumerable<LocalizedString>> GetAllStringsAsync(
        this IStringLocalizer stringLocalizer,
        bool includeParentCultures,
        bool includeBaseLocalizers,
        bool includeDynamicContributors)
    {
        var internalLocalizer = ((IStringLocalizer)ProxyHelper.UnProxy(stringLocalizer)).GetInternalLocalizer();
        if (internalLocalizer is ISmartSoftwareStringLocalizer ssStringLocalizer)
        {
            return await ssStringLocalizer.GetAllStringsAsync(
                includeParentCultures,
                includeBaseLocalizers,
                includeDynamicContributors
            );
        }

        return stringLocalizer.GetAllStrings(
            includeParentCultures
        );
    }

    public static async Task<IEnumerable<string>> GetSupportedCulturesAsync(this IStringLocalizer localizer)
    {
        var internalLocalizer = ((IStringLocalizer)ProxyHelper.UnProxy(localizer)).GetInternalLocalizer();
        if (internalLocalizer is ISmartSoftwareStringLocalizer ssStringLocalizer)
        {
            return await ssStringLocalizer.GetSupportedCulturesAsync();
        }

        return Array.Empty<string>();
    }
    
    public static Task<IEnumerable<LocalizedString>> GetAllStringsAsync(
        this IStringLocalizer localizer)
    {
        return localizer.GetAllStringsAsync(includeParentCultures: true);
    }
    
    public static Task<IEnumerable<LocalizedString>> GetAllStringsAsync(
        this IStringLocalizer localizer,
        bool includeParentCultures)
    {
        Check.NotNull(localizer, nameof(localizer));
        
        var internalLocalizer = ((IStringLocalizer)ProxyHelper.UnProxy(localizer)).GetInternalLocalizer();
        if (internalLocalizer is ISmartSoftwareStringLocalizer ssStringLocalizer)
        {
            return ssStringLocalizer.GetAllStringsAsync(includeParentCultures: includeParentCultures);
        }

        return Task.FromResult(
            localizer.GetAllStrings(includeParentCultures: true)
        );
    }
}
