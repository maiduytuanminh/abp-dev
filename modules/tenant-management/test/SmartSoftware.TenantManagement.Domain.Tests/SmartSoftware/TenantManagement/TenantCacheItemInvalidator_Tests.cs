using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.Caching;
using SmartSoftware.MultiTenancy;
using Xunit;

namespace SmartSoftware.TenantManagement;

public class TenantConfigurationCacheItemInvalidator_Tests : SmartSoftwareTenantManagementDomainTestBase
{
    private readonly IDistributedCache<TenantConfigurationCacheItem> _cache;
    private readonly ITenantStore _tenantStore;
    private readonly ITenantRepository _tenantRepository;
    private readonly ITenantManager _tenantManager;
    private readonly ITenantNormalizer _tenantNormalizer;

    public TenantConfigurationCacheItemInvalidator_Tests()
    {
        _cache = GetRequiredService<IDistributedCache<TenantConfigurationCacheItem>>();
        _tenantStore = GetRequiredService<ITenantStore>();
        _tenantRepository = GetRequiredService<ITenantRepository>();
        _tenantManager = GetRequiredService<ITenantManager>();
        _tenantNormalizer = GetRequiredService<ITenantNormalizer>();
    }

    [Fact]
    public async Task Get_Tenant_Should_Cached()
    {
        var acme = await _tenantRepository.FindByNameAsync(_tenantNormalizer.NormalizeName("acme"));
        acme.ShouldNotBeNull();

        (await _cache.GetAsync(TenantConfigurationCacheItem.CalculateCacheKey(acme.Id, null))).ShouldBeNull();
        (await _cache.GetAsync(TenantConfigurationCacheItem.CalculateCacheKey(null, acme.NormalizedName))).ShouldBeNull();

        await _tenantStore.FindAsync(acme.Id);
        (await _cache.GetAsync(TenantConfigurationCacheItem.CalculateCacheKey(acme.Id, null))).ShouldNotBeNull();

        await _tenantStore.FindAsync(acme.NormalizedName);
        (await _cache.GetAsync(TenantConfigurationCacheItem.CalculateCacheKey(null, acme.NormalizedName))).ShouldNotBeNull();


        var smartsoftware = _tenantRepository.FindByName(_tenantNormalizer.NormalizeName("smartsoftware"));
        smartsoftware.ShouldNotBeNull();

        (_cache.Get(TenantConfigurationCacheItem.CalculateCacheKey(smartsoftware.Id, null))).ShouldBeNull();
        (_cache.Get(TenantConfigurationCacheItem.CalculateCacheKey(null, smartsoftware.NormalizedName))).ShouldBeNull();

        _tenantStore.Find(smartsoftware.Id);
        (_cache.Get(TenantConfigurationCacheItem.CalculateCacheKey(smartsoftware.Id, null))).ShouldNotBeNull();

        _tenantStore.Find(smartsoftware.NormalizedName);
        (_cache.Get(TenantConfigurationCacheItem.CalculateCacheKey(null, smartsoftware.NormalizedName))).ShouldNotBeNull();
    }

    [Fact]
    public async Task Cache_Should_Invalidator_When_Tenant_Changed()
    {
        var acme = await _tenantRepository.FindByNameAsync(_tenantNormalizer.NormalizeName("acme"));
        acme.ShouldNotBeNull();

        // FindAsync will cache tenant.
        await _tenantStore.FindAsync(acme.Id);
        await _tenantStore.FindAsync(acme.NormalizedName);

        (await _cache.GetAsync(TenantConfigurationCacheItem.CalculateCacheKey(acme.Id, null))).ShouldNotBeNull();
        (await _cache.GetAsync(TenantConfigurationCacheItem.CalculateCacheKey(null, acme.NormalizedName))).ShouldNotBeNull();

        await _tenantRepository.DeleteAsync(acme);

        (await _cache.GetAsync(TenantConfigurationCacheItem.CalculateCacheKey(acme.Id, null))).ShouldBeNull();
        (await _cache.GetAsync(TenantConfigurationCacheItem.CalculateCacheKey(null, acme.NormalizedName))).ShouldBeNull();


        var smartsoftware = await _tenantRepository.FindByNameAsync(_tenantNormalizer.NormalizeName("smartsoftware"));
        smartsoftware.ShouldNotBeNull();

        // Find will cache tenant.
        _tenantStore.Find(smartsoftware.Id);
        _tenantStore.Find(smartsoftware.NormalizedName);

        (_cache.Get(TenantConfigurationCacheItem.CalculateCacheKey(smartsoftware.Id, null))).ShouldNotBeNull();
        (_cache.Get(TenantConfigurationCacheItem.CalculateCacheKey(null, smartsoftware.NormalizedName))).ShouldNotBeNull();

        await _tenantRepository.DeleteAsync(smartsoftware);

        (_cache.Get(TenantConfigurationCacheItem.CalculateCacheKey(smartsoftware.Id, null))).ShouldBeNull();
        (_cache.Get(TenantConfigurationCacheItem.CalculateCacheKey(null, smartsoftware.NormalizedName))).ShouldBeNull();

        var ss = await _tenantRepository.FindByNameAsync(_tenantNormalizer.NormalizeName("ss"));
        ss.ShouldNotBeNull();

        // Find will cache tenant.
        await _tenantStore.FindAsync(ss.Id);
        await _tenantStore.FindAsync(ss.NormalizedName);

        (await _cache.GetAsync(TenantConfigurationCacheItem.CalculateCacheKey(ss.Id, null))).ShouldNotBeNull();
        (await _cache.GetAsync(TenantConfigurationCacheItem.CalculateCacheKey(null, ss.NormalizedName))).ShouldNotBeNull();

        await _tenantManager.ChangeNameAsync(ss, "ss2");
        await _tenantRepository.UpdateAsync(ss);

        (await _cache.GetAsync(TenantConfigurationCacheItem.CalculateCacheKey(ss.Id, null))).ShouldBeNull();
        (await _cache.GetAsync(TenantConfigurationCacheItem.CalculateCacheKey(null, _tenantNormalizer.NormalizeName("ss")))).ShouldBeNull();
        (await _cache.GetAsync(TenantConfigurationCacheItem.CalculateCacheKey(null, _tenantNormalizer.NormalizeName("ss2")))).ShouldBeNull();
    }
}
