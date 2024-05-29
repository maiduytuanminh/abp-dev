using System.Threading.Tasks;

namespace SmartSoftware.Cli.Bundling;

public interface IBundlingService
{
    Task BundleAsync(string directory, bool forceBuild, string projectType = BundlingConsts.WebAssembly);
}
