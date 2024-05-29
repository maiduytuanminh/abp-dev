﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartSoftware.Json;
using SmartSoftware.Cli.Http;
using SmartSoftware.Cli.ProjectModification;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Threading;

namespace SmartSoftware.Cli.ProjectBuilding;

public class NpmPackageInfoProvider : INpmPackageInfoProvider, ITransientDependency
{
    public IJsonSerializer JsonSerializer { get; }
    public ICancellationTokenProvider CancellationTokenProvider { get; }
    public IRemoteServiceExceptionHandler RemoteServiceExceptionHandler { get; }

    private readonly CliHttpClientFactory _cliHttpClientFactory;

    public NpmPackageInfoProvider(
        IJsonSerializer jsonSerializer,
        ICancellationTokenProvider cancellationTokenProvider,
        IRemoteServiceExceptionHandler remoteServiceExceptionHandler,
        CliHttpClientFactory cliHttpClientFactory)
    {
        JsonSerializer = jsonSerializer;
        CancellationTokenProvider = cancellationTokenProvider;
        RemoteServiceExceptionHandler = remoteServiceExceptionHandler;
        _cliHttpClientFactory = cliHttpClientFactory;
    }

    public async Task<NpmPackageInfo> GetAsync(string name)
    {
        var packageList = await GetPackageListInternalAsync();

        var package = packageList.FirstOrDefault(m => m.Name == name);

        if (package == null)
        {
            throw new Exception("Package is not found or downloadable!");
        }

        return package;
    }

    private async Task<List<NpmPackageInfo>> GetPackageListInternalAsync()
    {
        var client = _cliHttpClientFactory.CreateClient();

        using (var responseMessage = await client.GetAsync(
            $"{CliUrls.WwwSmartSoftwareIo}api/download/npmPackages/",
            CancellationTokenProvider.Token
        ))
        {
            await RemoteServiceExceptionHandler.EnsureSuccessfulHttpResponseAsync(responseMessage);
            var result = await responseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<NpmPackageInfo>>(result);
        }
    }
}
