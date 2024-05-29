using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling;

public interface IBundleManager
{
    Task<IReadOnlyList<BundleFile>> GetStyleBundleFilesAsync(string bundleName);

    Task<IReadOnlyList<BundleFile>> GetScriptBundleFilesAsync(string bundleName);
}
