using System.Threading.Tasks;

namespace SmartSoftware.Cli.LIbs;

public interface IInstallLibsService
{
    Task InstallLibsAsync(string directory);
}
