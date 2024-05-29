using SmartSoftware.Cli.Bundling;

namespace SmartSoftware.Cli.Configuration;

public class SmartSoftwareCliConfig
{
    public BundleConfig Bundle { get; set; } = new();
}
