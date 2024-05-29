namespace SmartSoftware.Cli.Build;

public interface IDotNetProjectBuildConfigReader
{
    DotNetProjectBuildConfig Read(string directoryPath);
}
