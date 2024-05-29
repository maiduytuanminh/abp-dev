using SmartSoftware.Cli.ProjectBuilding.Templates.Module;

namespace SmartSoftware.Cli.ProjectBuilding.Templates.MvcModule;

public class ModuleTemplate : ModuleTemplateBase
{
    /// <summary>
    /// "module".
    /// </summary>
    public const string TemplateName = "module";

    public ModuleTemplate()
        : base(TemplateName)
    {
        DocumentUrl = "https://docs.smartsoftware.io/en/ss/latest/Startup-Templates/Module";
    }
}
