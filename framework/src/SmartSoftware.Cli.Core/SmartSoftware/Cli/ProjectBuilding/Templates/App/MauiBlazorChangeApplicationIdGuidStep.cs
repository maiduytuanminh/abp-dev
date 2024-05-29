using System;
using System.Linq;
using System.Xml;
using SmartSoftware.Cli.ProjectBuilding.Building;
using SmartSoftware.Cli.Utils;

namespace SmartSoftware.Cli.ProjectBuilding.Templates.App;

public class MauiBlazorChangeApplicationIdGuidStep: ProjectBuildPipelineStep
{
    public override void Execute(ProjectBuildContext context)
    {
        var projectFile = context.Files.FirstOrDefault(f => f.Name.EndsWith("MyCompanyName.MyProjectName.MauiBlazor.csproj"));
        
        if (projectFile == null)
        {
            return;
        }

        using (var stream = StreamHelper.GenerateStreamFromString(projectFile.Content))
        {
            var doc = new XmlDocument { PreserveWhitespace = true };
            doc.Load(stream);
                
            var node = doc.SelectSingleNode("/Project/PropertyGroup/ApplicationIdGuid");
            if (node != null)
            {
                node.InnerText = Guid.NewGuid().ToString();
            }
                
            projectFile.SetContent(doc.OuterXml);
        }
    }
}