using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;
using SmartSoftware.Caching;
using SmartSoftware.Http.Client;
using SmartSoftware.Http.Client.Authentication;
using SmartSoftware.Security.Claims;
using SmartSoftware.Users;

namespace SmartSoftware.AspNetCore.Mvc.Client;

public class RemoteDynamicClaimsPrincipalContributorCache : RemoteDynamicClaimsPrincipalContributorCacheBase<RemoteDynamicClaimsPrincipalContributorCache>
{
    public const string HttpClientName = nameof(RemoteDynamicClaimsPrincipalContributorCache);

    protected IDistributedCache<SmartSoftwareDynamicClaimCacheItem> Cache { get; }
    protected IHttpClientFactory HttpClientFactory { get; }
    protected IRemoteServiceHttpClientAuthenticator HttpClientAuthenticator { get; }
    protected IDistributedCache<ApplicationConfigurationDto> ApplicationConfigurationDtoCache { get; }
    protected ICurrentUser CurrentUser { get; }

    public RemoteDynamicClaimsPrincipalContributorCache(
        IDistributedCache<SmartSoftwareDynamicClaimCacheItem> cache,
        IHttpClientFactory httpClientFactory,
        IOptions<SmartSoftwareClaimsPrincipalFactoryOptions> ssClaimsPrincipalFactoryOptions,
        IRemoteServiceHttpClientAuthenticator httpClientAuthenticator,
        IDistributedCache<ApplicationConfigurationDto> applicationConfigurationDtoCache,
        ICurrentUser currentUser)
        : base(ssClaimsPrincipalFactoryOptions)
    {
        Cache = cache;
        HttpClientFactory = httpClientFactory;
        HttpClientAuthenticator = httpClientAuthenticator;
        ApplicationConfigurationDtoCache = applicationConfigurationDtoCache;
        CurrentUser = currentUser;
    }

    protected async override Task<SmartSoftwareDynamicClaimCacheItem?> GetCacheAsync(Guid userId, Guid? tenantId = null)
    {
        return await Cache.GetAsync(SmartSoftwareDynamicClaimCacheItem.CalculateCacheKey(userId, tenantId));
    }

    protected async override Task RefreshAsync(Guid userId, Guid? tenantId = null)
    {
        try
        {
            var client = HttpClientFactory.CreateClient(HttpClientName);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, SmartSoftwareClaimsPrincipalFactoryOptions.Value.RemoteRefreshUrl);
            await HttpClientAuthenticator.Authenticate(new RemoteServiceHttpClientAuthenticateContext(client, requestMessage, new RemoteServiceConfiguration("/"), string.Empty));
            var response = await client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            Logger.LogWarning(e, $"Failed to refresh remote claims for user: {userId}");
            await ApplicationConfigurationDtoCache.RemoveAsync(MvcCachedApplicationConfigurationClientHelper.CreateCacheKey(CurrentUser));
            throw;
        }
    }
}
