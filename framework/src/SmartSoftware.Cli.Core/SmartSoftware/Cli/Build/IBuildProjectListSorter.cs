using System.Collections.Generic;

namespace SmartSoftware.Cli.Build;

public interface IBuildProjectListSorter
{
    List<DotNetProjectInfo> SortByDependencies(
        List<DotNetProjectInfo> source,
        IEqualityComparer<DotNetProjectInfo> comparer = null);
}
