using System.Threading.Tasks;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.UI.Navigation;

public class FakePermissionStore : IPermissionStore, ITransientDependency
{
    public Task<bool> IsGrantedAsync(string name, string providerName, string providerKey)
    {
        var result = (name.Contains("Administration") || name.Contains("Dashboard")) && !name.Contains("SubMenu1");
        return Task.FromResult(result);
    }

    public Task<MultiplePermissionGrantResult> IsGrantedAsync(string[] names, string providerName, string providerKey)
    {
        var result = new MultiplePermissionGrantResult();
        foreach (var name in names)
        {
            result.Result.Add(name, (name.Contains("Administration") || name.Contains("Dashboard")) && !name.Contains("SubMenu1")
                ? PermissionGrantResult.Granted
                : PermissionGrantResult.Prohibited);
        }

        return Task.FromResult(result);
    }
}
