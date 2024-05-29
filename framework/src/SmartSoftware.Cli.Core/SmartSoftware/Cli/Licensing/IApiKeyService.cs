using System.Threading.Tasks;

namespace SmartSoftware.Cli.Licensing;

public interface IApiKeyService
{
    Task<DeveloperApiKeyResult> GetApiKeyOrNullAsync(bool invalidateCache = false);
}
