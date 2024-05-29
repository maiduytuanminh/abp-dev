using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using StackExchange.Redis;
using SmartSoftware.Cli.Args;
using SmartSoftware.Cli.Bundling;
using SmartSoftware.Cli.Commands.Services;
using SmartSoftware.Cli.LIbs;
using SmartSoftware.Cli.ProjectBuilding;
using SmartSoftware.Cli.ProjectBuilding.Building;
using SmartSoftware.Cli.ProjectBuilding.Events;
using SmartSoftware.Cli.ProjectBuilding.Templates.App;
using SmartSoftware.Cli.ProjectModification;
using SmartSoftware.Cli.Utils;
using SmartSoftware.Cli.Version;
using SmartSoftware.DependencyInjection;
using SmartSoftware.EventBus.Local;

namespace SmartSoftware.Cli.Commands;

public class NewCommand : ProjectCreationCommandBase, IConsoleCommand, ITransientDependency
{
    public const string Name = "new";

    protected TemplateProjectBuilder TemplateProjectBuilder { get; }
    public ITemplateInfoProvider TemplateInfoProvider { get; }

    public NewCommand(
        ConnectionStringProvider connectionStringProvider,
        SolutionPackageVersionFinder solutionPackageVersionFinder,
        ICmdHelper cmdHelper,
        IInstallLibsService installLibsService,
        CliService cliService,
        AngularPwaSupportAdder angularPwaSupportAdder,
        InitialMigrationCreator initialMigrationCreator,
        ThemePackageAdder themePackageAdder,
        ILocalEventBus eventBus,
        IBundlingService bundlingService,
        ITemplateInfoProvider templateInfoProvider,
        TemplateProjectBuilder templateProjectBuilder,
        AngularThemeConfigurer angularThemeConfigurer,
        CliVersionService cliVersionService) :
        base(connectionStringProvider,
            solutionPackageVersionFinder,
            cmdHelper,
            installLibsService,
            cliService,
            angularPwaSupportAdder,
            initialMigrationCreator,
            themePackageAdder,
            eventBus,
            bundlingService,
            angularThemeConfigurer,
            cliVersionService)
    {
        TemplateInfoProvider = templateInfoProvider;
        TemplateProjectBuilder = templateProjectBuilder;
    }

    public async Task ExecuteAsync(CommandLineArgs commandLineArgs)
    {
        var projectName = NamespaceHelper.NormalizeNamespace(commandLineArgs.Target);
        if (string.IsNullOrWhiteSpace(projectName))
        {
            throw new CliUsageException("Project name is missing!" + Environment.NewLine + Environment.NewLine + GetUsageInfo());
        }

        ProjectNameValidator.Validate(projectName);

        Logger.LogInformation("Creating your project...");
        Logger.LogInformation("Project name: " + projectName);

        var template = commandLineArgs.Options.GetOrNull(Options.Template.Short, Options.Template.Long);
        if (template != null)
        {
            Logger.LogInformation("Template: " + template);
        }
        else
        {
            template = (await TemplateInfoProvider.GetDefaultAsync()).Name;
        }

        var isTiered = commandLineArgs.Options.ContainsKey(Options.Tiered.Long);
        if (isTiered)
        {
            Logger.LogInformation("Tiered: yes");
        }

        var projectArgs = await GetProjectBuildArgsAsync(commandLineArgs, template, projectName);

        await CheckCreatingRequirements(projectArgs);

        var result = await TemplateProjectBuilder.BuildAsync(
            projectArgs
        );

        ExtractProjectZip(result, projectArgs.OutputFolder);

        Logger.LogInformation($"'{projectName}' has been successfully created to '{projectArgs.OutputFolder}'");

        await CheckCreatedRequirements(projectArgs);

        await CreateOpenIddictPfxFilesAsync(projectArgs);
        await RunGraphBuildForMicroserviceServiceTemplate(projectArgs);
        await CreateInitialMigrationsAsync(projectArgs);

        await ConfigureAngularAfterMicroserviceServiceCreatedAsync(projectArgs, template);

        var skipInstallLibs = commandLineArgs.Options.ContainsKey(Options.SkipInstallingLibs.Long) || commandLineArgs.Options.ContainsKey(Options.SkipInstallingLibs.Short);
        if (!skipInstallLibs)
        {
            await RunInstallLibsForWebTemplateAsync(projectArgs);
            ConfigureAngularJsonForThemeSelection(projectArgs);
        }

        var skipBundling = commandLineArgs.Options.ContainsKey(Options.SkipBundling.Long) || commandLineArgs.Options.ContainsKey(Options.SkipBundling.Short);
        if (!skipBundling)
        {
            await RunBundleInternalAsync(projectArgs);
        }

        await ConfigurePwaSupportForAngular(projectArgs);

        if (!commandLineArgs.Options.ContainsKey(Options.NoOpenWebPage.Long))
        {
            OpenRelatedWebPage(projectArgs, template, isTiered, commandLineArgs);
        }
    }

    private Task CheckCreatingRequirements(ProjectBuildArgs projectArgs)
    {
        return Task.CompletedTask;
    }

    private async Task CheckCreatedRequirements(ProjectBuildArgs projectArgs)
    {
        var requirementWarningMessages = new List<string>();

        if (projectArgs.ExtraProperties.ContainsKey("PreRequirements:Redis"))
        {
            var isConnected = false;
            try
            {
                var redis = await ConnectionMultiplexer.ConnectAsync("127.0.0.1", options => options.ConnectTimeout = 3000);
                isConnected = redis.IsConnected;
            }
            catch (Exception e)
            {
                // ignored
            }
            finally
            {
                if (!isConnected)
                {
                    requirementWarningMessages.Add("\t* Redis is not installed or not running on your computer.");
                }
            }
        }

        if (requirementWarningMessages.Any())
        {
            requirementWarningMessages.AddFirst("NOTICE: The following tools are required to run your solution:");

            await EventBus.PublishAsync(new ProjectPostRequirementsCheckedEvent
            {
                Message = requirementWarningMessages.JoinAsString(Environment.NewLine)
            }, false);

            foreach (var error in requirementWarningMessages)
            {
                Logger.LogWarning(error);
            }
        }
    }

    public string GetUsageInfo()
    {
        var sb = new StringBuilder();

        sb.AppendLine("");
        sb.AppendLine("Usage:");
        sb.AppendLine("");
        sb.AppendLine("  ss new <project-name> [options]");
        sb.AppendLine("");
        sb.AppendLine("Options:");
        sb.AppendLine("");
        sb.AppendLine("-t|--template <template-name>               (default: app)");
        sb.AppendLine("-u|--ui <ui-framework>                      (if supported by the template)");
        sb.AppendLine("-m|--mobile <mobile-framework>              (if supported by the template)");
        sb.AppendLine("-d|--database-provider <database-provider>  (if supported by the template)");
        sb.AppendLine("-o|--output-folder <output-folder>          (default: current folder)");
        sb.AppendLine("-v|--version <version>                      (default: latest version)");
        sb.AppendLine("--preview                                   (Use latest pre-release version if there is at least one pre-release after latest stable version)");
        sb.AppendLine("-ts|--template-source <template-source>     (your local or network ss template source)");
        sb.AppendLine("-csf|--create-solution-folder               (default: true)");
        sb.AppendLine("-cs|--connection-string <connection-string> (your database connection string)");
        sb.AppendLine("--dbms <database-management-system>         (your database management system)");
        sb.AppendLine("--theme <theme-name>                        (if supported by the template. default: leptonx-lite)");
        sb.AppendLine("--tiered                                    (if supported by the template)");
        sb.AppendLine("--no-ui                                     (if supported by the template)");
        sb.AppendLine("--no-random-port                            (Use template's default ports)");
        sb.AppendLine("--separate-auth-server                      (if supported by the template)");
        sb.AppendLine("--local-framework-ref --ss-path <your-local-ss-repo-path>  (keeps local references to projects instead of replacing with NuGet package references)");
        sb.AppendLine("-sib|--skip-installing-libs                      (Doesn't run `ss install-libs` command after project creation)");
        sb.AppendLine("-sb|--skip-bundling                             (Doesn't run `ss bundle` command after Blazor Wasm project creation)");
        sb.AppendLine("-sc|--skip-cache                                (Always download the latest from our server and refresh their templates folder cache)");
        sb.AppendLine("");
        sb.AppendLine("Examples:");
        sb.AppendLine("");
        sb.AppendLine("  ss new Acme.BookStore");
        sb.AppendLine("  ss new Acme.BookStore --tiered");
        sb.AppendLine("  ss new Acme.BookStore -u angular");
        sb.AppendLine("  ss new Acme.BookStore -u angular -d mongodb");
        sb.AppendLine("  ss new Acme.BookStore -m none");
        sb.AppendLine("  ss new Acme.BookStore -m react-native");
        sb.AppendLine("  ss new Acme.BookStore -d mongodb");
        sb.AppendLine("  ss new Acme.BookStore -d mongodb -o d:\\my-project");
        sb.AppendLine("  ss new Acme.BookStore -t module");
        sb.AppendLine("  ss new Acme.BookStore -t module --no-ui");
        sb.AppendLine("  ss new Acme.BookStore -t console");
        sb.AppendLine("  ss new Acme.BookStore -ts \"D:\\localTemplate\\ss\"");
        sb.AppendLine("  ss new Acme.BookStore -csf false");
        sb.AppendLine("  ss new Acme.BookStore --local-framework-ref --ss-path \"D:\\github\\ss\"");
        sb.AppendLine("  ss new Acme.BookStore --dbms mysql");
        sb.AppendLine("  ss new Acme.BookStore --theme basic");
        sb.AppendLine("  ss new Acme.BookStore --connection-string \"Server=myServerName\\myInstanceName;Database=myDatabase;User Id=myUsername;Password=myPassword\"");
        sb.AppendLine("");
        sb.AppendLine("See the documentation for more info: https://docs.smartsoftware.io/en/ss/latest/CLI");

        return sb.ToString();
    }

    public string GetShortDescription()
    {
        return "Generate a new solution based on the SS startup templates.";
    }
}
