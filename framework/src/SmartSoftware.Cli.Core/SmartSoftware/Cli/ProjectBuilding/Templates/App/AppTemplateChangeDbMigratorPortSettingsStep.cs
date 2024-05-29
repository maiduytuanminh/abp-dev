using SmartSoftware.Cli.ProjectBuilding.Building;
using SmartSoftware.Cli.ProjectBuilding.Files;

namespace SmartSoftware.Cli.ProjectBuilding.Templates.App;

public class AppTemplateChangeDbMigratorPortSettingsStep : ProjectBuildPipelineStep
{
    public string AuthServerPort { get; }

    /// <param name="authServerPort"></param>
    public AppTemplateChangeDbMigratorPortSettingsStep(
        string authServerPort)
    {
        AuthServerPort = authServerPort;
    }

    public override void Execute(ProjectBuildContext context)
    {
        context
            .GetFile("/aspnet-core/src/MyCompanyName.MyProjectName.DbMigrator/appsettings.json")
            .ReplaceText("44305", AuthServerPort);
    }
}
