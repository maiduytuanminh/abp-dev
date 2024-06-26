using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.PermissionManagement;

public class PermissionFinder : IPermissionFinder, ITransientDependency
{
    protected IPermissionManager PermissionManager { get; }

    public PermissionFinder(IPermissionManager permissionManager)
    {
        PermissionManager = permissionManager;
    }

    public virtual async Task<List<IsGrantedResponse>> IsGrantedAsync(List<IsGrantedRequest> requests)
    {
        var result = new List<IsGrantedResponse>();
        foreach (var item in requests)
        {
            result.Add(new IsGrantedResponse
            {
                UserId = item.UserId,
                Permissions = (await PermissionManager.GetAsync(item.PermissionNames, UserPermissionValueProvider.ProviderName, item.UserId.ToString())).Result
                    .ToDictionary(x => x.Name, x => x.IsGranted)
            });
        }

        return result;
    }
}
