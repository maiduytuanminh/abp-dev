using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartSoftware.AspNetCore.Components.Web.Theming.Bundling;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Components.Server.Theming.Bundling;

public class BlazorServerComponentBundleManager : IComponentBundleManager, ITransientDependency
{
    protected IBundleManager BundleManager { get; }

    public BlazorServerComponentBundleManager(IBundleManager bundleManager)
    {
        BundleManager = bundleManager;
    }

    public virtual async Task<IReadOnlyList<string>> GetStyleBundleFilesAsync(string bundleName)
    {
        return (await BundleManager.GetStyleBundleFilesAsync(bundleName)).Select(f => f.FileName).ToList();
    }

    public virtual async Task<IReadOnlyList<string>> GetScriptBundleFilesAsync(string bundleName)
    {
        return (await BundleManager.GetScriptBundleFilesAsync(bundleName)).Select(f => f.FileName).ToList();
    }
}
