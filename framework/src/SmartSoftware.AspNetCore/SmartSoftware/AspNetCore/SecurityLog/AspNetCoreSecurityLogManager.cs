﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartSoftware.AspNetCore.WebClientInfo;
using SmartSoftware.Clients;
using SmartSoftware.DependencyInjection;
using SmartSoftware.MultiTenancy;
using SmartSoftware.SecurityLog;
using SmartSoftware.Timing;
using SmartSoftware.Tracing;
using SmartSoftware.Users;

namespace SmartSoftware.AspNetCore.SecurityLog;

[Dependency(ReplaceServices = true)]
public class AspNetCoreSecurityLogManager : DefaultSecurityLogManager
{
    protected ILogger<AspNetCoreSecurityLogManager> Logger { get; }
    protected IClock Clock { get; }
    protected ICurrentUser CurrentUser { get; }
    protected ICurrentTenant CurrentTenant { get; }
    protected ICurrentClient CurrentClient { get; }
    protected IHttpContextAccessor HttpContextAccessor { get; }
    protected ICorrelationIdProvider CorrelationIdProvider { get; }
    protected IWebClientInfoProvider WebClientInfoProvider { get; }

    public AspNetCoreSecurityLogManager(
        IOptions<SmartSoftwareSecurityLogOptions> securityLogOptions,
        ISecurityLogStore securityLogStore,
        ILogger<AspNetCoreSecurityLogManager> logger,
        IClock clock,
        ICurrentUser currentUser,
        ICurrentTenant currentTenant,
        ICurrentClient currentClient,
        IHttpContextAccessor httpContextAccessor,
        ICorrelationIdProvider correlationIdProvider,
        IWebClientInfoProvider webClientInfoProvider)
        : base(securityLogOptions, securityLogStore)
    {
        Logger = logger;
        Clock = clock;
        CurrentUser = currentUser;
        CurrentTenant = currentTenant;
        CurrentClient = currentClient;
        HttpContextAccessor = httpContextAccessor;
        CorrelationIdProvider = correlationIdProvider;
        WebClientInfoProvider = webClientInfoProvider;
    }

    protected override async Task<SecurityLogInfo> CreateAsync()
    {
        var securityLogInfo = await base.CreateAsync();

        securityLogInfo.CreationTime = Clock.Now;

        securityLogInfo.TenantId = CurrentTenant.Id;
        securityLogInfo.TenantName = CurrentTenant.Name;

        securityLogInfo.UserId = CurrentUser.Id;
        securityLogInfo.UserName = CurrentUser.UserName;

        securityLogInfo.ClientId = CurrentClient.Id;

        securityLogInfo.CorrelationId = CorrelationIdProvider.Get();

        securityLogInfo.ClientIpAddress = WebClientInfoProvider.ClientIpAddress;
        securityLogInfo.BrowserInfo = WebClientInfoProvider.BrowserInfo;

        return securityLogInfo;
    }
}
