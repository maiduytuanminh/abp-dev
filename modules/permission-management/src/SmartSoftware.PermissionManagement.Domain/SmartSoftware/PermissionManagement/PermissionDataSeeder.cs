using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Guids;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.PermissionManagement;

public class PermissionDataSeeder : IPermissionDataSeeder, ITransientDependency
{
    protected IPermissionGrantRepository PermissionGrantRepository { get; }
    protected IGuidGenerator GuidGenerator { get; }

    protected ICurrentTenant CurrentTenant { get; }

    public PermissionDataSeeder(
        IPermissionGrantRepository permissionGrantRepository,
        IGuidGenerator guidGenerator,
        ICurrentTenant currentTenant)
    {
        PermissionGrantRepository = permissionGrantRepository;
        GuidGenerator = guidGenerator;
        CurrentTenant = currentTenant;
    }

    public virtual async Task SeedAsync(
        string providerName,
        string providerKey,
        IEnumerable<string> grantedPermissions,
        Guid? tenantId = null)
    {
        using (CurrentTenant.Change(tenantId))
        {
            var names = grantedPermissions.ToArray();
            var existsPermissionGrants = (await PermissionGrantRepository.GetListAsync(names, providerName, providerKey)).Select(x => x.Name).ToList();

            foreach (var permissionName in names.Except(existsPermissionGrants))
            {
                await PermissionGrantRepository.InsertAsync(
                    new PermissionGrant(
                        GuidGenerator.Create(),
                        permissionName,
                        providerName,
                        providerKey,
                        tenantId
                    )
                );
            }
        }
    }
}
