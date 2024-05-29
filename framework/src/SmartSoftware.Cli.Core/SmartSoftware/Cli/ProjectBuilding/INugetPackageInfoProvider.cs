using System.Threading.Tasks;
using SmartSoftware.Cli.ProjectModification;

namespace SmartSoftware.Cli.ProjectBuilding;

public interface INugetPackageInfoProvider
{
    Task<NugetPackageInfo> GetAsync(string name);
}
