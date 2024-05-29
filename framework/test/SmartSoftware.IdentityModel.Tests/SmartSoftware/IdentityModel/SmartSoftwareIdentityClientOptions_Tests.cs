﻿using System;
using Microsoft.Extensions.Options;
using Shouldly;
using SmartSoftware.MultiTenancy;
using Xunit;

namespace SmartSoftware.IdentityModel;

public class SmartSoftwareIdentityClientOptions_Tests : SmartSoftwareIdentityModelTestBase
{
    private readonly ICurrentTenant _currentTenant;
    private readonly SmartSoftwareIdentityClientOptions _identityClientOptions;

    public SmartSoftwareIdentityClientOptions_Tests()
    {
        _currentTenant = GetRequiredService<ICurrentTenant>();
        _identityClientOptions = GetRequiredService<IOptions<SmartSoftwareIdentityClientOptions>>().Value;
    }

    [Fact]
    public void GetClientConfiguration_Test()
    {
        var hostDefaultConfiguration = _identityClientOptions.GetClientConfiguration(_currentTenant);
        hostDefaultConfiguration.UserName.ShouldBe("host_default_admin");

        var hostIdentityConfiguration = _identityClientOptions.GetClientConfiguration(_currentTenant, "Identity");
        hostIdentityConfiguration.UserName.ShouldBe("host_identity_admin");

        using (_currentTenant.Change(Guid.Parse("f72a344f-651e-49f0-85f6-be260a10e4df"), "Test_Tenant1"))
        {
            var tenantDefaultConfiguration = _identityClientOptions.GetClientConfiguration(_currentTenant);
            tenantDefaultConfiguration.UserName.ShouldBe("tenant_default_admin");
        }

        using (_currentTenant.Change(Guid.Parse("f72a344f-651e-49f0-85f6-be260a10e4df")))
        {
            var tenantIdentityConfiguration = _identityClientOptions.GetClientConfiguration(_currentTenant, "Identity");
            tenantIdentityConfiguration.UserName.ShouldBe("tenant_identity_admin");
        }

        using (_currentTenant.Change(Guid.NewGuid()))
        {
            var configuration = _identityClientOptions.GetClientConfiguration(_currentTenant);
            configuration.UserName.ShouldBe("host_default_admin");

            configuration = _identityClientOptions.GetClientConfiguration(_currentTenant, "Identity");
            configuration.UserName.ShouldBe("host_identity_admin");
        }
    }
}
