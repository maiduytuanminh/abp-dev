using System.Collections.Generic;

namespace SmartSoftware.Cli.Build;

public interface IBuildStatusGenerator
{
    GitRepositoryBuildStatus Generate(
        DotNetProjectBuildConfig buildConfig,
        List<DotNetProjectInfo> changedProjects,
        List<string> buildSucceededProjects
    );
}
