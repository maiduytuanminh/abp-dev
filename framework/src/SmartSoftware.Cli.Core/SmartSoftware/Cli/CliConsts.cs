namespace SmartSoftware.Cli;

public static class CliConsts
{
    public const string Command = "SmartSoftwareCliCommand";

    public const string BranchPrefix = "branch@";

    public const string DocsLink = "https://docs.smartsoftware.io";

    public const string HttpClientName = "SmartSoftwareHttpClient";

    public const string GithubHttpClientName = "GithubHttpClient";

    public const string LogoutUrl = CliUrls.WwwSmartSoftwareIo + "api/license/logout";

    public const string LicenseCodePlaceHolder = @"<LICENSE_CODE/>";

    public const string AppSettingsJsonFileName = "appsettings.json";

    public const string AppSettingsSecretJsonFileName = "appsettings.secrets.json";
    
    public static class MemoryKeys
    {
        public const string LatestCliVersionCheckDate = "LatestCliVersionCheckDate";
    }
}
