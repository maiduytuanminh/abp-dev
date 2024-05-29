using System.Collections.Generic;

namespace SmartSoftware.Cli.Build;

public interface IDotNetProjectDependencyFiller
{
    void Fill(List<DotNetProjectInfo> projects);
}
