using System.Collections.Generic;

namespace LocalizationKeySynchronizer;

public class SmartSoftwareAsyncLocalization
{
    public SmartSoftwareAsyncLocalization(SmartSoftwareLocalization localization, SmartSoftwareLocalization reference, List<SmartSoftwareAsyncKey> asyncKeys)
    {
        Localization = localization;
        Reference = reference;
        AsyncKeys = asyncKeys;
    }

    public SmartSoftwareLocalization Localization { get; set; }
    public SmartSoftwareLocalization Reference { get; set; }

    public List<SmartSoftwareAsyncKey> AsyncKeys { get; set; }
}