using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Polly;
using Polly.Extensions.Http;
using SmartSoftware.Cli.Auth;
using SmartSoftware.Cli.Http;
using SmartSoftware.Cli.ProjectBuilding;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Json;
using SmartSoftware.Threading;

namespace SmartSoftware.Cli.Licensing;

public class SmartSoftwareIoApiKeyService : IApiKeyService, ITransientDependency
{
    protected IJsonSerializer JsonSerializer { get; }
    protected IRemoteServiceExceptionHandler RemoteServiceExceptionHandler { get; }
    protected ICancellationTokenProvider CancellationTokenProvider { get; }

    private readonly ILogger<SmartSoftwareIoApiKeyService> _logger;
    private DeveloperApiKeyResult _apiKeyResult = null;
    private readonly CliHttpClientFactory _cliHttpClientFactory;

    public SmartSoftwareIoApiKeyService(
        IJsonSerializer jsonSerializer,
        ICancellationTokenProvider cancellationTokenProvider,
        IRemoteServiceExceptionHandler remoteServiceExceptionHandler,
        ILogger<SmartSoftwareIoApiKeyService> logger,
        CliHttpClientFactory cliHttpClientFactory)
    {
        JsonSerializer = jsonSerializer;
        RemoteServiceExceptionHandler = remoteServiceExceptionHandler;
        _logger = logger;
        _cliHttpClientFactory = cliHttpClientFactory;
        CancellationTokenProvider = cancellationTokenProvider;
    }

    public async Task<DeveloperApiKeyResult> GetApiKeyOrNullAsync(bool invalidateCache = false)
    {
        if (!AuthService.IsLoggedIn())
        {
            return null;
        }

        if (invalidateCache)
        {
            _apiKeyResult = null;
        }

        if (_apiKeyResult != null)
        {
            return _apiKeyResult;
        }

        var url = $"{CliUrls.WwwSmartSoftwareIo}api/license/api-key";
        var client = _cliHttpClientFactory.CreateClient();

        using (var response = await client.GetHttpResponseMessageWithRetryAsync(url, CancellationTokenProvider.Token, _logger))
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"ERROR: Remote server returns '{response.StatusCode}'");
            }

            await RemoteServiceExceptionHandler.EnsureSuccessfulHttpResponseAsync(response);

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<DeveloperApiKeyResult>(responseContent);
        }

    }
}
