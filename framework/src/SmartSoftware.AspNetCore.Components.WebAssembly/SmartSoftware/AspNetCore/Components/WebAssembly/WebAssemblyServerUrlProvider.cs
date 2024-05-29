using System;
using System.Threading.Tasks;
using SmartSoftware.AspNetCore.Components.Web;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Http.Client;

namespace SmartSoftware.AspNetCore.Components.WebAssembly;

[Dependency(ReplaceServices = true)]
public class WebAssemblyServerUrlProvider : IServerUrlProvider, ITransientDependency
{
    protected IRemoteServiceConfigurationProvider RemoteServiceConfigurationProvider { get; }

    public WebAssemblyServerUrlProvider(
        IRemoteServiceConfigurationProvider remoteServiceConfigurationProvider)
    {
        RemoteServiceConfigurationProvider = remoteServiceConfigurationProvider;
    }

    public async Task<string> GetBaseUrlAsync(string? remoteServiceName = null)
    {
        var remoteServiceConfiguration = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultAsync(
            remoteServiceName ?? RemoteServiceConfigurationDictionary.DefaultName
        );

        return remoteServiceConfiguration.BaseUrl.EnsureEndsWith('/');
    }
}
