using Microsoft.CodeAnalysis;
using SmartSoftware.Modularity;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.TextTemplating.Razor;

[DependsOn(
    typeof(SmartSoftwareTextTemplatingTestModule)
)]
public class RazorTextTemplatingTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<RazorTextTemplatingTestModule>("SmartSoftware.TextTemplating.Razor");
        });

        Configure<SmartSoftwareRazorTemplateCSharpCompilerOptions>(options =>
        {
            options.References.Add(MetadataReference.CreateFromFile(typeof(RazorTextTemplatingTestModule).Assembly.Location));
        });
    }
}
