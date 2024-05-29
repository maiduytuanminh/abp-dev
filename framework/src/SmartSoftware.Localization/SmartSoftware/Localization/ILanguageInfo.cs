﻿namespace SmartSoftware.Localization;

public interface ILanguageInfo
{
    string CultureName { get; }

    string UiCultureName { get; }

    string DisplayName { get; }
}
