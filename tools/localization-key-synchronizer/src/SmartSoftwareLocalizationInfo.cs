using System.Collections.Generic;

namespace LocalizationKeySynchronizer;

// This class is used to deserialize the JSON string from culture file.
public class SmartSoftwareLocalizationInfo
{
    public SmartSoftwareLocalizationInfo(string culture, Dictionary<string, string> texts)
    {
        Culture = culture;
        Texts = texts;
    }

    public string Culture { get; set; }
    public Dictionary<string, string> Texts { get; set; }

    public static bool TryDeserialize(string json, out SmartSoftwareLocalizationInfo? localizationInfo)
    {
        return JsonHelper.TryDeserialize(json, out localizationInfo);
    }
}