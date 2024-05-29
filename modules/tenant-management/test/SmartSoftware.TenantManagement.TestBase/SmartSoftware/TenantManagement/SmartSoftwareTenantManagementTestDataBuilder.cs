using System.Threading.Tasks;
using SmartSoftware.Data;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Threading;

namespace SmartSoftware.TenantManagement;

public class SmartSoftwareTenantManagementTestDataBuilder : ITransientDependency
{
    private readonly ITenantRepository _tenantRepository;
    private readonly ITenantManager _tenantManager;

    public SmartSoftwareTenantManagementTestDataBuilder(
        ITenantRepository tenantRepository,
        ITenantManager tenantManager)
    {
        _tenantRepository = tenantRepository;
        _tenantManager = tenantManager;
    }

    public void Build()
    {
        AsyncHelper.RunSync(AddTenantsAsync);
    }

    private async Task AddTenantsAsync()
    {
        var acme = await _tenantManager.CreateAsync("acme");
        acme.ConnectionStrings.Add(new TenantConnectionString(acme.Id, ConnectionStrings.DefaultConnectionStringName, "DefaultConnString-Value"));
        acme.ConnectionStrings.Add(new TenantConnectionString(acme.Id, "MyConnString", "MyConnString-Value"));
        await _tenantRepository.InsertAsync(acme);

        var smartsoftware = await _tenantManager.CreateAsync("smartsoftware");
        await _tenantRepository.InsertAsync(smartsoftware);

        var ss = await _tenantManager.CreateAsync("ss");
        await _tenantRepository.InsertAsync(ss);
    }
}
