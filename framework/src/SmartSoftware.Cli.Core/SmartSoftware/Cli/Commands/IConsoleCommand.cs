using System.Threading.Tasks;
using SmartSoftware.Cli.Args;

namespace SmartSoftware.Cli.Commands;

public interface IConsoleCommand
{
    Task ExecuteAsync(CommandLineArgs commandLineArgs);

    string GetUsageInfo();

    string GetShortDescription();
}
