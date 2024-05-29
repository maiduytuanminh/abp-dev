﻿using SmartSoftware.Cli.ProjectBuilding.Templates.Microservice;

namespace SmartSoftware.Cli.ProjectBuilding.Building.Steps;

public class SolutionRenameStep : ProjectBuildPipelineStep
{
    public override void Execute(ProjectBuildContext context)
    {
        if (MicroserviceServiceTemplateBase.IsMicroserviceServiceTemplate(context.BuildArgs.TemplateName))
        {
            new SolutionRenamer(
                context.Files,
                "MyCompanyName.MyProjectName",
                "MicroserviceName",
                context.BuildArgs.SolutionName.CompanyName,
                context.BuildArgs.SolutionName.ProjectName
            ).Run();

            new SolutionRenamer(
                context.Files,
                "myCompanyName.myProjectName",
                "MicroserviceName",
                context.BuildArgs.SolutionName.CompanyName,
                context.BuildArgs.SolutionName.ProjectName
            ).Run();

            new SolutionRenamer(
                context.Files,
                null,
                "MyProjectName",
                null,
                SolutionName.Parse(context.BuildArgs.SolutionName.CompanyName).ProjectName
            ).Run();
        }
        else
        {
            new SolutionRenamer(
                context.Files,
                "MyCompanyName",
                "MyProjectName",
                context.BuildArgs.SolutionName.CompanyName,
                context.BuildArgs.SolutionName.ProjectName
            ).Run();
        }
    }
}