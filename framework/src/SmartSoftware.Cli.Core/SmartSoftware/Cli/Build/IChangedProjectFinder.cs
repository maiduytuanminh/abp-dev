using System.Collections.Generic;

namespace SmartSoftware.Cli.Build;

public interface IChangedProjectFinder
{
    List<DotNetProjectInfo> FindByRepository(DotNetProjectBuildConfig buildConfig);
}
