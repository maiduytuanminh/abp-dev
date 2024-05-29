using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using SmartSoftware.Cli.Args;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Cli.Commands;

public class HelpCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "help";
    
    public ILogger<HelpCommand> Logger { get; set; }
    protected SmartSoftwareCliOptions SmartSoftwareCliOptions { get; }
    protected IServiceScopeFactory ServiceScopeFactory { get; }

    public HelpCommand(IOptions<SmartSoftwareCliOptions> cliOptions,
        IServiceScopeFactory serviceScopeFactory)
    {
        ServiceScopeFactory = serviceScopeFactory;
        Logger = NullLogger<HelpCommand>.Instance;
        SmartSoftwareCliOptions = cliOptions.Value;
    }

    public Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        if (string.IsNullOrWhiteSpace(commandLineArgs.Target))
        {
            Logger.LogInformation(GetUsageInfo());
            return Task.CompletedTask;
        }

        if (!SmartSoftwareCliOptions.Commands.ContainsKey(commandLineArgs.Target))
        {
            Logger.LogWarning($"There is no command named {commandLineArgs.Target}.");
            Logger.LogInformation(GetUsageInfo());
            return Task.CompletedTask;
        }

        var commandType = SmartSoftwareCliOptions.Commands[commandLineArgs.Target];

        using (var scope = ServiceScopeFactory.CreateScope())
        {
            var command = (IConsoleCommand)scope.ServiceProvider.GetRequiredService(commandType);
            Logger.LogInformation(command.GetUsageInfo());
        }

        return Task.CompletedTask;
    }

    public string GetUsageInfo()
    {
        var sb = new StringBuilder();

        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("");
        sb.AppendLine("    ss <command> <target> [options]");
        sb.AppendLine("");
        sb.AppendLine("Command List:");
        sb.AppendLine("");

        foreach (var command in SmartSoftwareCliOptions.Commands.ToArray())
        {
            string shortDescription;

            using (var scope = ServiceScopeFactory.CreateScope())
            {
                shortDescription = ((IConsoleCommand)scope.ServiceProvider
                        .GetRequiredService(command.Value)).GetShortDescription();
            }

            sb.Append("    > ");
            sb.Append(command.Key);
            sb.Append(string.IsNullOrWhiteSpace(shortDescription) ? "" : ":");
            sb.Append(" ");
            sb.AppendLine(shortDescription);
        }

        sb.AppendLine("");
        sb.AppendLine("To get a detailed help for a command:");
        sb.AppendLine("");
        sb.AppendLine("    ss help <command>");
        sb.AppendLine("");
        sb.AppendLine("See the documentation for more info: https://docs.smartsoftware.io/en/ss/latest/CLI");

        return sb.ToString();
    }

    public string GetShortDescription()
    {
        return "Show command line help. Write ` ss help <command> `";
    }
}
