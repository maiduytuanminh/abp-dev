﻿using System;
using System.Collections.Generic;
using SmartSoftware.Collections;

namespace SmartSoftware.Localization;

public class SmartSoftwareLocalizationOptions
{
    public LocalizationResourceDictionary Resources { get; }

    /// <summary>
    /// Used as the default resource when resource was not specified on a localization operation.
    /// </summary>
    public Type? DefaultResourceType { get; set; }

    public ITypeList<ILocalizationResourceContributor> GlobalContributors { get; }

    public List<LanguageInfo> Languages { get; }

    public Dictionary<string, List<NameValue>> LanguagesMap { get; }

    public Dictionary<string, List<NameValue>> LanguageFilesMap { get; }

    public bool TryToGetFromBaseCulture { get; set; }

    public bool TryToGetFromDefaultCulture { get; set; }

    public SmartSoftwareLocalizationOptions()
    {
        Resources = new LocalizationResourceDictionary();
        GlobalContributors = new TypeList<ILocalizationResourceContributor>();
        Languages = new List<LanguageInfo>();
        LanguagesMap = new Dictionary<string, List<NameValue>>();
        LanguageFilesMap = new Dictionary<string, List<NameValue>>();
        TryToGetFromBaseCulture = true;
        TryToGetFromDefaultCulture = true;
    }
}
