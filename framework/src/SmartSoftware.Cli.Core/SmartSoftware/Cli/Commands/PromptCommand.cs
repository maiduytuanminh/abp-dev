using System.Text;
using System.Threading.Tasks;
using SmartSoftware.Cli.Args;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Cli.Commands;

public class PromptCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "prompt";
    
    public Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        return Task.CompletedTask;
    }

    public string GetUsageInfo()
    {
        var sb = new StringBuilder();

        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("  ss prompt");
        sb.AppendLine("");
        sb.AppendLine("See the documentation for more info: https://docs.smartsoftware.io/en/ss/latest/CLI");

        return sb.ToString();
    }

    public string GetShortDescription()
    {
        return "Starts with prompt mode.";
    }
}
