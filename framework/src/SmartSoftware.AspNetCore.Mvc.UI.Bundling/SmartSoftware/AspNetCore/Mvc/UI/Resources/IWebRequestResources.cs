using System.Collections.Generic;
using SmartSoftware.AspNetCore.Mvc.UI.Bundling;

namespace SmartSoftware.AspNetCore.Mvc.UI.Resources;

public interface IWebRequestResources
{
    List<BundleFile> TryAdd(List<BundleFile> resources);
}
