using SmartSoftware.Cli.ProjectBuilding.Building.Steps;
using SmartSoftware.Cli.ProjectBuilding.Templates;

namespace SmartSoftware.Cli.ProjectBuilding.Building;

public static class NugetPackageProjectBuildPipelineBuilder
{
    public static ProjectBuildPipeline Build(ProjectBuildContext context)
    {
        var pipeline = new ProjectBuildPipeline(context);

        pipeline.Steps.Add(new FileEntryListReadStep());
        pipeline.Steps.Add(new ProjectReferenceReplaceStep());
        pipeline.Steps.Add(new ReplaceCommonPropsStep());
        pipeline.Steps.Add(new ReplaceConfigureAwaitPropsStep());
        pipeline.Steps.Add(new CreateProjectResultZipStep());

        return pipeline;
    }
}
