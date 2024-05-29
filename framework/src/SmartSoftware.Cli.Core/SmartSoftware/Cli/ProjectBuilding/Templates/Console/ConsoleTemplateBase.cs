using JetBrains.Annotations;
using SmartSoftware.Cli.ProjectBuilding.Building;

namespace SmartSoftware.Cli.ProjectBuilding.Templates.Console;

public abstract class ConsoleTemplateBase : TemplateInfo
{
    protected ConsoleTemplateBase([NotNull] string name) :
        base(name)
    {
    }
}
