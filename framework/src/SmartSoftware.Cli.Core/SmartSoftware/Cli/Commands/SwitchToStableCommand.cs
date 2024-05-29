using System.Text;
using System.Threading.Tasks;
using SmartSoftware.Cli.Args;
using SmartSoftware.Cli.ProjectModification;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Cli.Commands;

public class SwitchToStableCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "switch-to-stable";
    
    private readonly PackagePreviewSwitcher _packagePreviewSwitcher;

    public SwitchToStableCommand(PackagePreviewSwitcher packagePreviewSwitcher)
    {
        _packagePreviewSwitcher = packagePreviewSwitcher;
    }

    public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        await _packagePreviewSwitcher.SwitchToStable(commandLineArgs);
    }

    public string GetUsageInfo()
    {
        var sb = new StringBuilder();

        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("  ss switch-to-stable [options]");
        sb.AppendLine("");
        sb.AppendLine("Options:");
        sb.AppendLine("-d|--directory");
        sb.AppendLine("");
        sb.AppendLine("See the documentation for more info: https://docs.smartsoftware.io/en/ss/latest/CLI");

        return sb.ToString();
    }

    public string GetShortDescription()
    {
        return "Switches packages to stable SS version from preview version.";
    }
}
