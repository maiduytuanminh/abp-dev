using System;
using System.Linq;
using SmartSoftware.Cli.ProjectBuilding.Files;

namespace SmartSoftware.Cli.ProjectBuilding.Building;

public class ConnectionStringRenameStep : ProjectBuildPipelineStep
{
    public override void Execute(ProjectBuildContext context)
    {
        foreach (var fileEntry in context.Files.Where(file => file.Name.EndsWith(CliConsts.AppSettingsJsonFileName, StringComparison.OrdinalIgnoreCase)))
        {
            RenameDatabaseName(fileEntry);
        }
    }

    private void RenameDatabaseName(FileEntry fileEntry)
    {
        fileEntry.SetContent(fileEntry.Content.Replace("MyProjectNamePro", "MyProjectName"));
    }
}