using System.Threading.Tasks;
using SmartSoftware.Cli.ProjectBuilding.Building;

namespace SmartSoftware.Cli.ProjectBuilding;

public interface ITemplateInfoProvider
{
    Task<TemplateInfo> GetDefaultAsync();

    TemplateInfo Get(string name);
}
