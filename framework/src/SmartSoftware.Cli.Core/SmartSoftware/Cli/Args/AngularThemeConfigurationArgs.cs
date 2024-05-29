using SmartSoftware.Cli.ProjectBuilding.Building;

namespace SmartSoftware.Cli.Args;

public class AngularThemeConfigurationArgs 
{
    public Theme Theme { get; }

    public string ProjectName { get; }

    public string AngularFolderPath { get; }

    public AngularThemeConfigurationArgs(Theme theme, string projectName, string angularFolderPath)
    {
        Theme = theme;
        ProjectName = projectName;
        AngularFolderPath = angularFolderPath;
    }
}