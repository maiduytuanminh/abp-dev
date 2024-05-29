using System;
using Microsoft.Extensions.Localization;

namespace SmartSoftware.Localization;

public interface ISmartSoftwareEnumLocalizer
{
    string GetString(Type enumType, object enumValue);

    string GetString(Type enumType, object enumValue, IStringLocalizer?[] specifyLocalizers);
}
