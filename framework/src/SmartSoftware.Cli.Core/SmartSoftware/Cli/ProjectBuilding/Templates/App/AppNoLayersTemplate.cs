using SmartSoftware.Cli.ProjectBuilding.Building;

namespace SmartSoftware.Cli.ProjectBuilding.Templates.App;

public class AppNoLayersTemplate : AppNoLayersTemplateBase
{
    /// <summary>
    /// "app-nolayers".
    /// </summary>
    public const string TemplateName = "app-nolayers";

    public const Theme DefaultTheme = Theme.LeptonXLite;

    public AppNoLayersTemplate()
        : base(TemplateName)
    {
        //TODO: Change URL
        //DocumentUrl = CliConsts.DocsLink + "/en/ss/latest/Startup-Templates/Application";
    }
}
