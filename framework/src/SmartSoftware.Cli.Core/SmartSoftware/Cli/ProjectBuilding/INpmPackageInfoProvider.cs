using System.Threading.Tasks;
using SmartSoftware.Cli.ProjectModification;

namespace SmartSoftware.Cli.ProjectBuilding;

public interface INpmPackageInfoProvider
{
    Task<NpmPackageInfo> GetAsync(string name);
}
