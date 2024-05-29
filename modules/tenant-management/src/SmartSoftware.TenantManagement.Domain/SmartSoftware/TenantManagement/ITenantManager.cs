using System.Threading.Tasks;
using JetBrains.Annotations;
using SmartSoftware.Domain.Services;

namespace SmartSoftware.TenantManagement;

public interface ITenantManager : IDomainService
{
    [NotNull]
    Task<Tenant> CreateAsync([NotNull] string name);

    Task ChangeNameAsync([NotNull] Tenant tenant, [NotNull] string name);
}
