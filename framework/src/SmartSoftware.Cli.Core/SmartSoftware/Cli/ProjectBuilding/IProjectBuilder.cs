using System.Threading.Tasks;

namespace SmartSoftware.Cli.ProjectBuilding;

public interface IProjectBuilder
{
    Task<ProjectBuildResult> BuildAsync(ProjectBuildArgs args);
}
