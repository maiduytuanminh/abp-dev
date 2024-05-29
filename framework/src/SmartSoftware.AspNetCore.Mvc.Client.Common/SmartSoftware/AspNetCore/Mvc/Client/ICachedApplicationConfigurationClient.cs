using System.Threading.Tasks;
using SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;

namespace SmartSoftware.AspNetCore.Mvc.Client;

public interface ICachedApplicationConfigurationClient
{
    Task<ApplicationConfigurationDto> GetAsync();

    ApplicationConfigurationDto Get();
}
