﻿using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using SmartSoftware.SecurityLog;
using SmartSoftware.Testing;
using Xunit;

namespace SmartSoftware.Security.SecurityLog;

public class SecurityLogManager_Tests : SmartSoftwareIntegratedTest<SmartSoftwareSecurityTestModule>
{
    private readonly ISecurityLogManager _securityLogManager;

    private ISecurityLogStore _auditingStore;

    public SecurityLogManager_Tests()
    {
        _securityLogManager = GetRequiredService<ISecurityLogManager>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        _auditingStore = Substitute.For<ISecurityLogStore>();
        services.AddSingleton(_auditingStore);
    }

    [Fact]
    public async Task SaveAsync()
    {
        await _securityLogManager.SaveAsync(securityLog =>
        {
            securityLog.Identity = "Test";
            securityLog.Action = "Test-Action";
            securityLog.UserName = "Test-User";
        });

        await _auditingStore.Received().SaveAsync(Arg.Is<SecurityLogInfo>(log =>
            log.ApplicationName == "SmartSoftwareSecurityTest" &&
            log.Identity == "Test" &&
            log.Action == "Test-Action" &&
            log.UserName == "Test-User"));
    }
}
