using System.Collections.Generic;
using JetBrains.Annotations;
using SmartSoftware.Cli.ProjectBuilding.Building;
using SmartSoftware.Cli.ProjectBuilding.Building.Steps;

namespace SmartSoftware.Cli.ProjectBuilding.Templates.Maui;

public class MauiTemplateBase: TemplateInfo
{
    protected MauiTemplateBase([NotNull] string name) :
        base(name)
    {
    }

    public override IEnumerable<ProjectBuildPipelineStep> GetCustomSteps(ProjectBuildContext context)
    {
        var steps = new List<ProjectBuildPipelineStep>
        {
            new MauiChangeApplicationIdGuidStep()
        };
        
        return steps;
    }
}