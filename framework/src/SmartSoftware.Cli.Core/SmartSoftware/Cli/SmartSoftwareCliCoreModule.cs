using System.Text;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Cli.Commands;
using SmartSoftware.Cli.Commands.Internal;
using SmartSoftware.Cli.Http;
using SmartSoftware.Cli.ServiceProxying;
using SmartSoftware.Cli.ServiceProxying.Angular;
using SmartSoftware.Cli.ServiceProxying.CSharp;
using SmartSoftware.Cli.ServiceProxying.JavaScript;
using SmartSoftware.Domain;
using SmartSoftware.Http;
using SmartSoftware.IdentityModel;
using SmartSoftware.Json;
using SmartSoftware.Localization;
using SmartSoftware.Minify;
using SmartSoftware.Modularity;

namespace SmartSoftware.Cli;

[DependsOn(
    typeof(SmartSoftwareDddDomainModule),
    typeof(SmartSoftwareJsonModule),
    typeof(SmartSoftwareIdentityModelModule),
    typeof(SmartSoftwareMinifyModule),
    typeof(SmartSoftwareHttpModule),
    typeof(SmartSoftwareLocalizationModule)
)]
public class SmartSoftwareCliCoreModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClient(CliConsts.HttpClientName)
            .ConfigurePrimaryHttpMessageHandler(() => new CliHttpClientHandler());

        context.Services.AddHttpClient(CliConsts.GithubHttpClientName, client =>
        {
            client.DefaultRequestHeaders.UserAgent.ParseAdd("MyAgent/1.0");
        });

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        Configure<SmartSoftwareCliOptions>(options =>
        {
            options.Commands[HelpCommand.Name] = typeof(HelpCommand);
            options.Commands[PromptCommand.Name] = typeof(PromptCommand);
            options.Commands[NewCommand.Name] = typeof(NewCommand);
            options.Commands[GetSourceCommand.Name] = typeof(GetSourceCommand);
            options.Commands[UpdateCommand.Name] = typeof(UpdateCommand);
            options.Commands[AddPackageCommand.Name] = typeof(AddPackageCommand);
            options.Commands[AddModuleCommand.Name] = typeof(AddModuleCommand);
            options.Commands[ListModulesCommand.Name] = typeof(ListModulesCommand);
            options.Commands[ListTemplatesCommand.Name] = typeof(ListTemplatesCommand);
            options.Commands[LoginCommand.Name] = typeof(LoginCommand);
            options.Commands[LoginInfoCommand.Name] = typeof(LoginInfoCommand);
            options.Commands[LogoutCommand.Name] = typeof(LogoutCommand);
            options.Commands[GenerateProxyCommand.Name] = typeof(GenerateProxyCommand);
            options.Commands[RemoveProxyCommand.Name] = typeof(RemoveProxyCommand);
            options.Commands[SuiteCommand.Name] = typeof(SuiteCommand);
            options.Commands[SwitchToPreviewCommand.Name] = typeof(SwitchToPreviewCommand);
            options.Commands[SwitchToStableCommand.Name] = typeof(SwitchToStableCommand);
            options.Commands[SwitchToNightlyCommand.Name] = typeof(SwitchToNightlyCommand);
            options.Commands[SwitchToPreRcCommand.Name] = typeof(SwitchToPreRcCommand);
            options.Commands[SwitchToLocal.Name] = typeof(SwitchToLocal);
            options.Commands[TranslateCommand.Name] = typeof(TranslateCommand);
            options.Commands[BuildCommand.Name] = typeof(BuildCommand);
            options.Commands[BundleCommand.Name] = typeof(BundleCommand);
            options.Commands[CreateMigrationAndRunMigratorCommand.Name] = typeof(CreateMigrationAndRunMigratorCommand);
            options.Commands[InstallLibsCommand.Name] = typeof(InstallLibsCommand);
            options.Commands[CleanCommand.Name] = typeof(CleanCommand);
            options.Commands[CliCommand.Name] = typeof(CliCommand);
            options.Commands[ClearDownloadCacheCommand.Name] = typeof(ClearDownloadCacheCommand);
            options.Commands[RecreateInitialMigrationCommand.Name] = typeof(RecreateInitialMigrationCommand);

            options.DisabledModulesToAddToSolution.Add("SmartSoftware.LeptonXTheme.Pro");
            options.DisabledModulesToAddToSolution.Add("SmartSoftware.LeptonXTheme.Lite");
        });

        Configure<SmartSoftwareCliServiceProxyOptions>(options =>
        {
            options.Generators[JavaScriptServiceProxyGenerator.Name] = typeof(JavaScriptServiceProxyGenerator);
            options.Generators[AngularServiceProxyGenerator.Name] = typeof(AngularServiceProxyGenerator);
            options.Generators[CSharpServiceProxyGenerator.Name] = typeof(CSharpServiceProxyGenerator);
        });
    }
}
