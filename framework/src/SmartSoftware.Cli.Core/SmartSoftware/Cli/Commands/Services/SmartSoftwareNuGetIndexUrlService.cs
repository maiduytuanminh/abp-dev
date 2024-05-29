using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SmartSoftware.Cli.Licensing;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Cli.Commands.Services;

public class SmartSoftwareNuGetIndexUrlService : ITransientDependency
{
    private readonly IApiKeyService _apiKeyService;
    public ILogger<SmartSoftwareNuGetIndexUrlService> Logger { get; set; }

    public SmartSoftwareNuGetIndexUrlService(IApiKeyService apiKeyService)
    {
        _apiKeyService = apiKeyService;
        Logger = NullLogger<SmartSoftwareNuGetIndexUrlService>.Instance;
    }

    public async Task<string> GetAsync()
    {
        var apiKeyResult = await _apiKeyService.GetApiKeyOrNullAsync();

        if (apiKeyResult == null)
        {
            Logger.LogWarning("You are not signed in! Use the CLI command \"ss login <username>\" to sign in, then try again.");
            return null;
        }

        if (!string.IsNullOrWhiteSpace(apiKeyResult.ErrorMessage))
        {
            Logger.LogWarning(apiKeyResult.ErrorMessage);
            return null;
        }

        if (string.IsNullOrEmpty(apiKeyResult.ApiKey))
        {
            Logger.LogError("Couldn't retrieve your NuGet API key! You can re-sign in with the CLI command \"ss login <username>\".");
            return null;
        }

        return CliUrls.GetNuGetServiceIndexUrl(apiKeyResult.ApiKey);
    }
}
