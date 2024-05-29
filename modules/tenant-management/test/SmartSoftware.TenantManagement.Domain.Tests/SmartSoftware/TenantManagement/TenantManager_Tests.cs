using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.MultiTenancy;
using Xunit;

namespace SmartSoftware.TenantManagement;

public class TenantManager_Tests : SmartSoftwareTenantManagementDomainTestBase
{
    private readonly ITenantManager _tenantManager;
    private readonly ITenantRepository _tenantRepository;
    private readonly ITenantNormalizer _tenantNormalizer;

    public TenantManager_Tests()
    {
        _tenantManager = GetRequiredService<ITenantManager>();
        _tenantRepository = GetRequiredService<ITenantRepository>();
        _tenantNormalizer = GetRequiredService<ITenantNormalizer>();
    }

    [Fact]
    public async Task CreateAsync()
    {
        var tenant = await _tenantManager.CreateAsync("Test");
        tenant.Name.ShouldBe("Test");
        tenant.NormalizedName.ShouldBe(_tenantNormalizer.NormalizeName("Test"));
    }

    [Fact]
    public async Task Create_Tenant_Name_Can_Not_Duplicate()
    {
        await Assert.ThrowsAsync<BusinessException>(async () => await _tenantManager.CreateAsync("smartsoftware"));
    }

    [Fact]
    public async Task ChangeNameAsync()
    {
        var tenant = await _tenantRepository.FindByNameAsync(_tenantNormalizer.NormalizeName("smartsoftware"));
        tenant.ShouldNotBeNull();
        tenant.NormalizedName.ShouldBe(_tenantNormalizer.NormalizeName("smartsoftware"));

        await _tenantManager.ChangeNameAsync(tenant, "newSmartSoftware");

        tenant.Name.ShouldBe("newSmartSoftware");
        tenant.NormalizedName.ShouldBe(_tenantNormalizer.NormalizeName("newSmartSoftware"));
    }

    [Fact]
    public async Task ChangeName_Tenant_Name_Can_Not_Duplicate()
    {
        var tenant = await _tenantRepository.FindByNameAsync(_tenantNormalizer.NormalizeName("acme"));
        tenant.ShouldNotBeNull();

        await Assert.ThrowsAsync<BusinessException>(async () => await _tenantManager.ChangeNameAsync(tenant, "smartsoftware"));
    }
}
