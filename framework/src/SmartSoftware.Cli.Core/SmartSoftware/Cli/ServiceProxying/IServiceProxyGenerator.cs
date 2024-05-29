using System.Threading.Tasks;

namespace SmartSoftware.Cli.ServiceProxying;

public interface IServiceProxyGenerator
{
    Task GenerateProxyAsync(GenerateProxyArgs args);
}
