using System;
using SmartSoftware.Http.Modeling;

namespace SmartSoftware.Cli;

public static class CliUrls
{
    public const string WwwSmartSoftwareIo = WwwSmartSoftwareIoProduction;
    public const string AccountSmartSoftwareIo = AccountSmartSoftwareIoProduction;
    public const string NuGetRootPath = NuGetRootPathProduction;
    public const string LatestVersionCheckFullPath =
        "https://raw.githubusercontent.com/ssframework/ss/dev/latest-versions.json";

    public const string WwwSmartSoftwareIoProduction = "https://smartsoftware.io/";
    public const string AccountSmartSoftwareIoProduction = "https://account.smartsoftware.io/";
    public const string NuGetRootPathProduction = "https://nuget.smartsoftware.io/";

    public const string WwwSmartSoftwareIoDevelopment = "https://localhost:44328/";
    public const string AccountSmartSoftwareIoDevelopment = "https://localhost:44333/";
    public const string NuGetRootPathDevelopment = "https://localhost:44373/";

    public static string GetNuGetServiceIndexUrl(string apiKey)
    {
        return $"{NuGetRootPath}{apiKey}/v3/index.json";
    }

    public static string GetNuGetPackageInfoUrl(string apiKey, string packageId)
    {
        return $"{NuGetRootPath}{apiKey}/v3/package/{packageId}/index.json";
    }

    public static string GetNuGetPackageSearchUrl(string apiKey, string packageId)
    {
        return $"{NuGetRootPath}{apiKey}/v3/search?q={packageId}";
    }

    public static string GetApiDefinitionUrl(string url, ApplicationApiDescriptionModelRequestDto model = null)
    {
        url = url.EnsureEndsWith('/');
        return $"{url}api/ss/api-definition{(model != null ? model.IncludeTypes ? "?includeTypes=true" : string.Empty : string.Empty)}";
    }
}
