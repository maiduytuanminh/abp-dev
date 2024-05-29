namespace LocalizationKeySynchronizer;

public class SmartSoftwareLocalization
{
    public SmartSoftwareLocalization(string filePath, SmartSoftwareLocalizationInfo localizationInfo)
    {
        FilePath = filePath;
        LocalizationInfo = localizationInfo;
    }

    public string FilePath { get; set; }

    public SmartSoftwareLocalizationInfo LocalizationInfo { get; set; }
}