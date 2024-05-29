using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using SmartSoftware.Autofac;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.TextTemplating.Localization;
using SmartSoftware.TextTemplating.Razor;
using SmartSoftware.TextTemplating.Scriban;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.TextTemplating;

[DependsOn(
    typeof(SmartSoftwareTextTemplatingScribanModule),
    typeof(SmartSoftwareTextTemplatingRazorModule),
    typeof(SmartSoftwareTestBaseModule),
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareLocalizationModule)
)]
public class SmartSoftwareTextTemplatingTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartSoftwareTextTemplatingTestModule>("SmartSoftware.TextTemplating");
        });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Add<TestLocalizationSource>("en")
                .AddVirtualJson("/Localization");
        });

        Configure<SmartSoftwareCompiledViewProviderOptions>(options =>
        {
            options.TemplateReferences.Add(TestTemplates.HybridTemplateRazor,
                new List<PortableExecutableReference>()
                {
                        MetadataReference.CreateFromFile(typeof(SmartSoftwareTextTemplatingTestModule).Assembly.Location)
                });
        });
    }
}
