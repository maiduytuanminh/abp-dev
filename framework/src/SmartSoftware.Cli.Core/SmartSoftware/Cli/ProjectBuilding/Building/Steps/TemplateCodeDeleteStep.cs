﻿using SmartSoftware.Cli.ProjectBuilding.Files;

namespace SmartSoftware.Cli.ProjectBuilding.Building.Steps;

public class TemplateCodeDeleteStep : ProjectBuildPipelineStep
{
    public override void Execute(ProjectBuildContext context)
    {
        foreach (var file in context.Files)
        {
            if (file.Name.EndsWith(".cs") ||
                file.Name.EndsWith(".csproj") ||
                file.Name.EndsWith(".cshtml") ||
                file.Name.EndsWith(".json") ||
                file.Name.EndsWith(".gitignore") ||
                file.Name.EndsWith(".yml") ||
                file.Name.EndsWith(".yaml") ||
                file.Name.EndsWith(".md") ||
                file.Name.EndsWith(".ps1") ||
                file.Name.EndsWith(".html") || 
                file.Name.EndsWith(".ts") ||
                file.Name.EndsWith(".css") ||
                file.Name.Contains("Dockerfile"))
            {
                file.RemoveTemplateCode(context.Symbols);
                file.RemoveTemplateCodeMarkers();
            }
        }
    }
}
