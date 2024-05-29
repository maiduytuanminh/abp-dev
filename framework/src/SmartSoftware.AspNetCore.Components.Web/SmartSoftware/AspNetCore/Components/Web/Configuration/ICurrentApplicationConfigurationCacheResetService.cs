using System.Threading.Tasks;

namespace SmartSoftware.AspNetCore.Components.Web.Configuration;

public interface ICurrentApplicationConfigurationCacheResetService
{
    Task ResetAsync();
}
