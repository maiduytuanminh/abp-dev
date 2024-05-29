using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SmartSoftware.Cli.Args;
using SmartSoftware.Cli.Commands.Services;
using SmartSoftware.Cli.Version;
using SmartSoftware.Cli.Utils;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Cli.Commands;

public class CliCommand : IConsoleCommand, ITransientDependency
{
    public const string Name = "cli";

    private const string CliPackageName = "SmartSoftware.Cli";

    private readonly ICmdHelper _cmdHelper;
    private readonly PackageVersionCheckerService _packageVersionCheckerService;
    private readonly SmartSoftwareNuGetIndexUrlService _nuGetIndexUrlService;
    public ILogger<CliCommand> Logger { get; set; }

    public CliCommand(ICmdHelper cmdHelper, PackageVersionCheckerService packageVersionCheckerService, SmartSoftwareNuGetIndexUrlService nuGetIndexUrlService)
    {
        _cmdHelper = cmdHelper;
        _packageVersionCheckerService = packageVersionCheckerService;
        _nuGetIndexUrlService = nuGetIndexUrlService;

        Logger = NullLogger<CliCommand>.Instance;
    }

    public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        var operationType = NamespaceHelper.NormalizeNamespace(commandLineArgs.Target);

        var preview = commandLineArgs.Options.ContainsKey(Options.Preview.Short) ||
                      commandLineArgs.Options.ContainsKey(Options.Preview.Long);

        var version = commandLineArgs.Options.GetOrNull(Options.Version.Short, Options.Version.Long);

        switch (operationType)
        {
            case "":
            case null:
                _cmdHelper.RunCmd("ss");
                break;

            case "update":
                await UpdateCliAsync(version, preview);
                break;

            case "remove":
                RemoveCli();
                break;
        }
    }

    private async Task UpdateCliAsync(string version = null, bool preview = false)
    {
        var infoText = "Updating SS CLI ";
        if (version != null)
        {
            infoText += "to the " + version + "... ";
        }
        else if (preview)
        {
            infoText += "to the latest preview version...";
        }
        else
        {
            infoText += "...";
        }

        Logger.LogInformation(infoText);

        try
        {
            var versionOption = string.Empty;

            if (preview)
            {
                var latestPreviewVersion = await GetLatestPreviewVersion();
                if (latestPreviewVersion != null)
                {
                    versionOption = $" --version {latestPreviewVersion}";
                    Logger.LogInformation("Latest preview version is " + latestPreviewVersion);
                }
            }
            else if (version != null)
            {
                versionOption = $" --version {version}";
            }

            _cmdHelper.RunCmdAndExit($"dotnet tool update {CliPackageName}{versionOption} -g", delaySeconds: 2);
        }
        catch (Exception ex)
        {
            Logger.LogError("Couldn't update SS CLI." + ex.Message);
            ShowCliManualUpdateCommand();
        }
    }

    private async Task<string> GetLatestPreviewVersion()
    {
        var latestPreviewVersionInfo = await _packageVersionCheckerService
            .GetLatestVersionOrNullAsync(
                packageId: CliPackageName,
                includeReleaseCandidates: true
            );

        return latestPreviewVersionInfo.Version.IsPrerelease ? latestPreviewVersionInfo.Version.ToString() : null;
    }

    private void ShowCliManualUpdateCommand()
    {
        Logger.LogError("You can also run the following command to update SS CLI.");
        Logger.LogError("dotnet tool update -g SmartSoftware.Cli");
    }

    private void RemoveCli()
    {
        Logger.LogInformation("Removing CLI...");
        _cmdHelper.RunCmdAndExit("dotnet tool uninstall " + CliPackageName + " -g", delaySeconds: 2);
    }

    public string GetUsageInfo()
    {
        var sb = new StringBuilder();

        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("");
        sb.AppendLine("  ss cli [options]");
        sb.AppendLine("");
        sb.AppendLine("Options:");
        sb.AppendLine("");
        sb.AppendLine("update                                 (update SS CLI to the latest)");
        sb.AppendLine("remove                                 (uninstall SS CLI)");
        sb.AppendLine("");
        sb.AppendLine("Examples:");
        sb.AppendLine("");
        sb.AppendLine("  ss cli update");
        sb.AppendLine("  ss cli update --preview");
        sb.AppendLine("  ss cli update --version 4.2.2");
        sb.AppendLine("  ss cli remove");
        sb.AppendLine("");

        return sb.ToString();
    }

    public string GetShortDescription()
    {
        return "Update or remove SS CLI. See https://docs.smartsoftware.io/en/ss/latest/CLI";
    }

    public static class Options
    {
        public static class Preview
        {
            public const string Long = "preview";
            public const string Short = "p";
        }

        public static class Version
        {
            public const string Long = "version";
            public const string Short = "v";
        }
    }
}
