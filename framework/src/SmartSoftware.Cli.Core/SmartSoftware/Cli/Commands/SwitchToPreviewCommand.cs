using System.Text;
using System.Threading.Tasks;
using SmartSoftware.Cli.Args;
using SmartSoftware.Cli.ProjectModification;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Cli.Commands;

public class SwitchToPreviewCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "switch-to-preview";
    
    private readonly PackagePreviewSwitcher _packagePreviewSwitcher;

    public SwitchToPreviewCommand(PackagePreviewSwitcher packagePreviewSwitcher)
    {
        _packagePreviewSwitcher = packagePreviewSwitcher;
    }

    public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        await _packagePreviewSwitcher.SwitchToPreview(commandLineArgs);
    }

    public string GetUsageInfo()
    {
        var sb = new StringBuilder();

        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("  ss switch-to-preview [options]");
        sb.AppendLine("");
        sb.AppendLine("Options:");
        sb.AppendLine("-d|--directory");
        sb.AppendLine("");
        sb.AppendLine("See the documentation for more info: https://docs.smartsoftware.io/en/ss/latest/CLI");

        return sb.ToString();
    }

    public string GetShortDescription()
    {
        return "Switches packages to preview SS version.";
    }
}
