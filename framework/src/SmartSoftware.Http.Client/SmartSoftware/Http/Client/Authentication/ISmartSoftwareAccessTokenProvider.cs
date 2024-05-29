using System.Threading.Tasks;

namespace SmartSoftware.Http.Client.Authentication;

public interface ISmartSoftwareAccessTokenProvider
{
    Task<string?> GetTokenAsync();
}
