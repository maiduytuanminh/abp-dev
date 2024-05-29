using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SmartSoftware.Localization;

public static class SmartSoftwareLocalizationOptionsExtensions
{
    public static SmartSoftwareLocalizationOptions AddLanguagesMapOrUpdate(this SmartSoftwareLocalizationOptions localizationOptions,
        string packageName, params NameValue[] maps)
    {
        foreach (var map in maps)
        {
            AddOrUpdate(localizationOptions.LanguagesMap, packageName, map);
        }

        return localizationOptions;
    }

    public static string GetLanguagesMap(this SmartSoftwareLocalizationOptions localizationOptions, string packageName,
        string language)
    {
        return localizationOptions.LanguagesMap.TryGetValue(packageName, out var maps)
            ? maps.FirstOrDefault(x => x.Name == language)?.Value ?? language
            : language;
    }

    public static string GetCurrentUICultureLanguagesMap(this SmartSoftwareLocalizationOptions localizationOptions, string packageName)
    {
        return GetLanguagesMap(localizationOptions, packageName, CultureInfo.CurrentUICulture.Name);
    }

    public static SmartSoftwareLocalizationOptions AddLanguageFilesMapOrUpdate(this SmartSoftwareLocalizationOptions localizationOptions,
        string packageName, params NameValue[] maps)
    {
        foreach (var map in maps)
        {
            AddOrUpdate(localizationOptions.LanguageFilesMap, packageName, map);
        }

        return localizationOptions;
    }

    public static string GetLanguageFilesMap(this SmartSoftwareLocalizationOptions localizationOptions, string packageName,
        string language)
    {
        return localizationOptions.LanguageFilesMap.TryGetValue(packageName, out var maps)
            ? maps.FirstOrDefault(x => x.Name == language)?.Value ?? language
            : language;
    }

    public static string GetCurrentUICultureLanguageFilesMap(this SmartSoftwareLocalizationOptions localizationOptions, string packageName)
    {
        return GetLanguageFilesMap(localizationOptions, packageName, CultureInfo.CurrentUICulture.Name);
    }

    private static void AddOrUpdate(IDictionary<string, List<NameValue>> maps, string packageName, NameValue value)
    {
        if (maps.TryGetValue(packageName, out var existMaps))
        {
            existMaps.GetOrAdd(x => x.Name == value.Name, () => value).Value = value.Value;
        }
        else
        {
            maps.Add(packageName, new List<NameValue> { value });
        }
    }
}
