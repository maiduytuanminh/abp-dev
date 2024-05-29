using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.Uow;
using Xunit;

namespace SmartSoftware.TenantManagement;

public abstract class LazyLoad_Tests<TStartupModule> : TenantManagementTestBase<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    public ITenantRepository TenantRepository { get; }
    public ITenantNormalizer TenantNormalizer { get; }

    protected LazyLoad_Tests()
    {
        TenantRepository = GetRequiredService<ITenantRepository>();
        TenantNormalizer = GetRequiredService<ITenantNormalizer>();
    }

    [Fact]
    public async Task Should_Lazy_Load_Tenant_Collections()
    {
        using (var uow = GetRequiredService<IUnitOfWorkManager>().Begin())
        {
            var role = await TenantRepository.FindByNameAsync(TenantNormalizer.NormalizeName("acme"), includeDetails: false);
            role.ConnectionStrings.ShouldNotBeNull();
            role.ConnectionStrings.Any().ShouldBeTrue();

            await uow.CompleteAsync();
        }
    }
}
