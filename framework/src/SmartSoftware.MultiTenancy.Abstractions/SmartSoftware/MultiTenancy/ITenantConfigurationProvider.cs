using System.Threading.Tasks;

namespace SmartSoftware.MultiTenancy;

public interface ITenantConfigurationProvider
{
    Task<TenantConfiguration?> GetAsync(bool saveResolveResult = false);
}
