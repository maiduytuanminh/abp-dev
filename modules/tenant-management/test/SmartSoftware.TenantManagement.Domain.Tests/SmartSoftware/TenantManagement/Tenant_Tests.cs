﻿using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.MultiTenancy;
using Xunit;

namespace SmartSoftware.TenantManagement;

public class Tenant_Tests : SmartSoftwareTenantManagementDomainTestBase
{
    private readonly ITenantRepository _tenantRepository;
    private readonly ITenantNormalizer _tenantNormalizer;

    public Tenant_Tests()
    {
        _tenantRepository = GetRequiredService<ITenantRepository>();
        _tenantNormalizer = GetRequiredService<ITenantNormalizer>();
    }

    [Fact]
    public async Task FindDefaultConnectionString()
    {
        var acme = await _tenantRepository.FindByNameAsync(_tenantNormalizer.NormalizeName("acme"));

        acme.ShouldNotBeNull();
        acme.FindDefaultConnectionString().ShouldBe("DefaultConnString-Value");
    }

    [Fact]
    public async Task FindConnectionString()
    {
        var acme = await _tenantRepository.FindByNameAsync(_tenantNormalizer.NormalizeName("acme"));

        acme.ShouldNotBeNull();
        acme.FindConnectionString(Data.ConnectionStrings.DefaultConnectionStringName).ShouldBe("DefaultConnString-Value");
        acme.FindConnectionString("MyConnString").ShouldBe("MyConnString-Value");
    }
}
