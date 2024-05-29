﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Components.Progression;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Components.MauiBlazor;

public class SmartSoftwareMauiBlazorClientHttpMessageHandler : DelegatingHandler, ITransientDependency
{
    private readonly IUiPageProgressService _uiPageProgressService;
    private readonly IMauiBlazorSelectedLanguageProvider _mauiBlazorSelectedLanguageProvider;

    public SmartSoftwareMauiBlazorClientHttpMessageHandler(
        IClientScopeServiceProviderAccessor clientScopeServiceProviderAccessor,
        IMauiBlazorSelectedLanguageProvider mauiBlazorSelectedLanguageProvider)
    {
        _mauiBlazorSelectedLanguageProvider = mauiBlazorSelectedLanguageProvider;
        _uiPageProgressService = clientScopeServiceProviderAccessor.ServiceProvider.GetRequiredService<IUiPageProgressService>();
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            await _uiPageProgressService.Go(null, options =>
            {
                options.Type = UiPageProgressType.Info;
            });

            await SetLanguageAsync(request);

            return await base.SendAsync(request, cancellationToken);
        }
        finally
        {
            await _uiPageProgressService.Go(-1);
        }
    }

    private async Task SetLanguageAsync(HttpRequestMessage request)
    {
        var selectedLanguage = await _mauiBlazorSelectedLanguageProvider.GetSelectedLanguageAsync();

        if (!selectedLanguage.IsNullOrWhiteSpace())
        {
            request.Headers.AcceptLanguage.Clear();
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(selectedLanguage!));
        }
    }
}