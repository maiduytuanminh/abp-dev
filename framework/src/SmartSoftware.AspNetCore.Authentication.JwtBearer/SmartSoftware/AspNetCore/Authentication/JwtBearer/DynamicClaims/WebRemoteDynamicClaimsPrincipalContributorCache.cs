using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartSoftware.Caching;
using SmartSoftware.Security.Claims;

namespace SmartSoftware.AspNetCore.Authentication.JwtBearer.DynamicClaims;

public class WebRemoteDynamicClaimsPrincipalContributorCache : RemoteDynamicClaimsPrincipalContributorCacheBase<WebRemoteDynamicClaimsPrincipalContributorCache>
{
    public const string HttpClientName = nameof(WebRemoteDynamicClaimsPrincipalContributorCache);

    protected IDistributedCache<SmartSoftwareDynamicClaimCacheItem> Cache { get; }
    protected IHttpClientFactory HttpClientFactory { get; }
    protected IHttpContextAccessor HttpContextAccessor { get; }
    protected IOptions<WebRemoteDynamicClaimsPrincipalContributorOptions> Options { get; }

    public WebRemoteDynamicClaimsPrincipalContributorCache(
        IDistributedCache<SmartSoftwareDynamicClaimCacheItem> cache,
        IHttpClientFactory httpClientFactory,
        IOptions<SmartSoftwareClaimsPrincipalFactoryOptions> ssClaimsPrincipalFactoryOptions,
        IHttpContextAccessor httpContextAccessor,
        IOptions<WebRemoteDynamicClaimsPrincipalContributorOptions> options)
        : base(ssClaimsPrincipalFactoryOptions)
    {
        Cache = cache;
        HttpClientFactory = httpClientFactory;
        HttpContextAccessor = httpContextAccessor;
        Options = options;
    }

    protected async override Task<SmartSoftwareDynamicClaimCacheItem?> GetCacheAsync(Guid userId, Guid? tenantId = null)
    {
        return await Cache.GetAsync(SmartSoftwareDynamicClaimCacheItem.CalculateCacheKey(userId, tenantId));
    }

    protected async override Task RefreshAsync(Guid userId, Guid? tenantId = null)
    {
        try
        {
            if (HttpContextAccessor.HttpContext == null)
            {
                throw new SmartSoftwareException($"Failed to refresh remote claims for user: {userId} - HttpContext is null!");
            }

            var authenticateResult = await HttpContextAccessor.HttpContext.AuthenticateAsync(Options.Value.AuthenticationScheme);
            if (!authenticateResult.Succeeded)
            {
                throw new SmartSoftwareException($"Failed to refresh remote claims for user: {userId} - authentication failed!");
            }

            var accessToken = authenticateResult.Properties?.GetTokenValue("access_token");
            if (accessToken.IsNullOrWhiteSpace())
            {
                throw new SmartSoftwareException($"Failed to refresh remote claims for user: {userId} - access_token is null or empty!");
            }

            var client = HttpClientFactory.CreateClient(HttpClientName);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, SmartSoftwareClaimsPrincipalFactoryOptions.Value.RemoteRefreshUrl);
            requestMessage.SetBearerToken(accessToken);
            var response = await client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            Logger.LogWarning(e, $"Failed to refresh remote claims for user: {userId}");
            throw;
        }
    }
}
