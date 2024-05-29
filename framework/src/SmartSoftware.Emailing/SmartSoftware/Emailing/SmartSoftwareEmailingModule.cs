using SmartSoftware.BackgroundJobs;
using SmartSoftware.Emailing.Localization;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.Settings;
using SmartSoftware.TextTemplating;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.Emailing;

[DependsOn(
    typeof(SmartSoftwareSettingsModule),
    typeof(SmartSoftwareVirtualFileSystemModule),
    typeof(SmartSoftwareBackgroundJobsAbstractionsModule),
    typeof(SmartSoftwareLocalizationModule),
    typeof(SmartSoftwareTextTemplatingModule)
    )]
public class SmartSoftwareEmailingModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareEmailingModule>();
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<EmailingResource>("en")
                .AddVirtualJson("/SmartSoftware/Emailing/Localization");
        });

        Configure<SmartSoftwareBackgroundJobOptions>(options =>
        {
            options.AddJob<BackgroundEmailSendingJob>();
        });
    }
}
