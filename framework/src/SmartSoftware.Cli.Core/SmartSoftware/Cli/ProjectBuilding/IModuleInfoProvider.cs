using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.Cli.ProjectBuilding.Building;

namespace SmartSoftware.Cli.ProjectBuilding;

public interface IModuleInfoProvider
{
    Task<ModuleInfo> GetAsync(string name);

    Task<List<ModuleInfo>> GetModuleListAsync();
}
