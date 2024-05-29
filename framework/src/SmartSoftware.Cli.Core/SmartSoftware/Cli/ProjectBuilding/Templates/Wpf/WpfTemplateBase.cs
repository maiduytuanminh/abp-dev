using JetBrains.Annotations;
using SmartSoftware.Cli.ProjectBuilding.Building;

namespace SmartSoftware.Cli.ProjectBuilding.Templates.Wpf;

public class WpfTemplateBase : TemplateInfo
{
    protected WpfTemplateBase([NotNull] string name) :
        base(name)
    {
    }
}
