namespace LocalizationKeySynchronizer;

public class MissingKey : SmartSoftwareAsyncKey
{
    public MissingKey(string key, string reference) : base(key, reference)
    {
    }
}