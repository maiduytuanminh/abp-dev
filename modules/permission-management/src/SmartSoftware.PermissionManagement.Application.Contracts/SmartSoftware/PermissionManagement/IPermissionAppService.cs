using System.Threading.Tasks;
using JetBrains.Annotations;
using SmartSoftware.Application.Services;

namespace SmartSoftware.PermissionManagement;

public interface IPermissionAppService : IApplicationService
{
    Task<GetPermissionListResultDto> GetAsync([NotNull] string providerName, [NotNull] string providerKey);

    Task UpdateAsync([NotNull] string providerName, [NotNull] string providerKey, UpdatePermissionsDto input);
}
