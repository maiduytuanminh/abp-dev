using System.Collections.Generic;

namespace LocalizationKeySynchronizer;

public class SmartSoftwareAsyncLocalizationViewModel
{
    public SmartSoftwareAsyncLocalizationViewModel(string referenceCulture, string culture, string path, List<SmartSoftwareAsyncKey> asyncKeys)
    {
        ReferenceCulture = referenceCulture;
        Culture = culture;
        Path = path;
        AsyncKeys = asyncKeys;
    }

    public string ReferenceCulture { get; set; }
    public string Culture { get; set; }
    public string Path { get; set; }

    public List<SmartSoftwareAsyncKey> AsyncKeys { get; set; }
}