using SmartSoftware.Bundling;

namespace SmartSoftware.Cli.Bundling;

public interface IBundler
{
    string Bundle(BundleOptions options, BundleContext context);
}
