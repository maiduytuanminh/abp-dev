﻿using System;
using System.Net.Http;
using System.Threading;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Threading;

namespace SmartSoftware.Cli.Http;

public class CliHttpClientFactory : ISingletonDependency
{
    public static readonly TimeSpan DefaultTimeout = TimeSpan.FromMinutes(2);

    private readonly IHttpClientFactory _clientFactory;
    private readonly ICancellationTokenProvider _cancellationTokenProvider;

    public CliHttpClientFactory(IHttpClientFactory clientFactory,
        ICancellationTokenProvider cancellationTokenProvider)
    {
        _clientFactory = clientFactory;
        _cancellationTokenProvider = cancellationTokenProvider;
    }

    public HttpClient CreateClient(bool needsAuthentication = true, TimeSpan? timeout = null, string clientName = null)
    {
        var httpClient = _clientFactory.CreateClient(clientName ?? CliConsts.HttpClientName);
        httpClient.Timeout = timeout ?? DefaultTimeout;

        if (needsAuthentication)
        {
            httpClient.AddSmartSoftwareAuthenticationToken();
        }

        return httpClient;
    }

    public CancellationToken GetCancellationToken(TimeSpan? timeout = null)
    {
        if (timeout == null)
        {
            if (_cancellationTokenProvider == null)
            {
                var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.CancelAfter(DefaultTimeout);
                return cancellationTokenSource.Token;
            }
            else
            {
                return _cancellationTokenProvider.Token;
            }
        }
        else
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(Convert.ToInt32(timeout.Value.TotalMilliseconds));
            return cancellationTokenSource.Token;
        }
    }
}
