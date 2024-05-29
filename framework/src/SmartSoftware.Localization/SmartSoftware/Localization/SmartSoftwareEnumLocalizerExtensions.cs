using System;
using Microsoft.Extensions.Localization;

namespace SmartSoftware.Localization;

public static class SmartSoftwareEnumLocalizerExtensions
{
    public static string GetString<TEnum>(this ISmartSoftwareEnumLocalizer ssEnumLocalizer, object enumValue)
        where TEnum : Enum
    {
        return ssEnumLocalizer.GetString(typeof(TEnum), enumValue);
    }

    public static string GetString<TEnum>(this ISmartSoftwareEnumLocalizer ssEnumLocalizer, object enumValue, IStringLocalizer[] specifyLocalizers)
        where TEnum : Enum
    {
        return ssEnumLocalizer.GetString(typeof(TEnum), enumValue, specifyLocalizers);
    }
}
