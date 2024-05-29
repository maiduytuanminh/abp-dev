using System.Threading.Tasks;

namespace SmartSoftware.MultiTenancy;

public interface ITenantResolveContributor
{
    string Name { get; }

    Task ResolveAsync(ITenantResolveContext context);
}
