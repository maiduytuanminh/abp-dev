using System.IO;

namespace SmartSoftware.Cli.ProjectModification;

public static class BlazorProjectTypeChecker
{
    public static bool IsBlazorServerProject(string blazorProjectPath)
    {
        var blazorProjectCsprojContent = File.ReadAllText(blazorProjectPath);

        return !blazorProjectCsprojContent.Contains("Microsoft.NET.Sdk.BlazorWebAssembly");
    }
}
