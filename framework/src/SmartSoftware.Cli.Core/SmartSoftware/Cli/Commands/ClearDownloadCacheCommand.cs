using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SmartSoftware.Cli.Args;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Cli.Commands;

public class ClearDownloadCacheCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "clear-download-cache";

    public ILogger<ClearDownloadCacheCommand> Logger { get; set; }

    public ClearDownloadCacheCommand()
    {
        Logger = NullLogger<ClearDownloadCacheCommand>.Instance;
    }

    public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        Logger.LogInformation("Clearing the templates download cache...");
        foreach (var cacheFile in Directory.GetFiles(CliPaths.TemplateCache, "*.zip"))
        {
            Logger.LogInformation($"Deleting {cacheFile}");
            try
            {
                File.Delete(cacheFile);
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"Could not delete {cacheFile}");
            }
        }
        Logger.LogInformation("Done.");
        await Task.CompletedTask;
    }

    public string GetUsageInfo()
    {
        var sb = new StringBuilder();

        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("  ss clear-download-cache");
        sb.AppendLine("");
        sb.AppendLine("See the documentation for more info: https://docs.smartsoftware.io/en/ss/latest/CLI");

        return sb.ToString();
    }

    public string GetShortDescription()
    {
        return "Clears the templates download cache.";
    }
}
