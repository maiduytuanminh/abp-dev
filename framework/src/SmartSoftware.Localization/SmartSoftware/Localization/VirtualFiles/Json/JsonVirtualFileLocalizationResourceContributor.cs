﻿using Microsoft.Extensions.FileProviders;
using System;
using SmartSoftware.Localization.Json;

namespace SmartSoftware.Localization.VirtualFiles.Json;

//TODO: Use composition over inheritance..?

public class JsonVirtualFileLocalizationResourceContributor : VirtualFileLocalizationResourceContributorBase
{
    public JsonVirtualFileLocalizationResourceContributor(string virtualPath)
        : base(virtualPath)
    {

    }

    protected override bool CanParseFile(IFileInfo file)
    {
        return file.Name.EndsWith(".json", StringComparison.OrdinalIgnoreCase);
    }

    protected override ILocalizationDictionary? CreateDictionaryFromFileContent(string jsonString)
    {
        return JsonLocalizationDictionaryBuilder.BuildFromJsonString(jsonString);
    }
}
