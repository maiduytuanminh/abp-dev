﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using Xunit;

namespace SmartSoftware.TenantManagement;

public abstract class TenantRepository_Tests<TStartupModule> : TenantManagementTestBase<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    public ITenantRepository TenantRepository { get; }
    public ITenantNormalizer TenantNormalizer { get; }

    protected TenantRepository_Tests()
    {
        TenantRepository = GetRequiredService<ITenantRepository>();
        TenantNormalizer = GetRequiredService<ITenantNormalizer>();
    }

    [Fact]
    public async Task FindByNameAsync()
    {
        var tenant = await TenantRepository.FindByNameAsync(TenantNormalizer.NormalizeName("acme"));
        tenant.ShouldNotBeNull();

        tenant = await TenantRepository.FindByNameAsync(TenantNormalizer.NormalizeName("undefined-tenant"));
        tenant.ShouldBeNull();

        tenant = await TenantRepository.FindByNameAsync(TenantNormalizer.NormalizeName("acme"), includeDetails: true);
        tenant.ShouldNotBeNull();
        tenant.ConnectionStrings.Count.ShouldBeGreaterThanOrEqualTo(2);
    }

    [Fact]
    public async Task FindAsync()
    {
        var tenantId = (await TenantRepository.FindByNameAsync(TenantNormalizer.NormalizeName("acme"))).Id;

        var tenant = await TenantRepository.FindAsync(tenantId);
        tenant.ShouldNotBeNull();

        tenant = await TenantRepository.FindAsync(Guid.NewGuid());
        tenant.ShouldBeNull();

        tenant = await TenantRepository.FindAsync(tenantId, includeDetails: true);
        tenant.ShouldNotBeNull();
        tenant.ConnectionStrings.Count.ShouldBeGreaterThanOrEqualTo(2);
    }

    [Fact]
    public async Task GetListAsync()
    {
        var tenants = await TenantRepository.GetListAsync();
        tenants.ShouldContain(t => t.Name == "acme" && t.NormalizedName == TenantNormalizer.NormalizeName("acme"));
        tenants.ShouldContain(t => t.Name == "smartsoftware" && t.NormalizedName == TenantNormalizer.NormalizeName("smartsoftware"));
    }

    [Fact]
    public async Task Should_Eager_Load_Tenant_Collections()
    {
        var role = await TenantRepository.FindByNameAsync(TenantNormalizer.NormalizeName("acme"));
        role.ConnectionStrings.ShouldNotBeNull();
        role.ConnectionStrings.Any().ShouldBeTrue();
    }
}
