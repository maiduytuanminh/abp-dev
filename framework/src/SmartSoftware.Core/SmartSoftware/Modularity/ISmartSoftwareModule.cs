using System.Threading.Tasks;

namespace SmartSoftware.Modularity;

public interface ISmartSoftwareModule
{
    Task ConfigureServicesAsync(ServiceConfigurationContext context);

    void ConfigureServices(ServiceConfigurationContext context);
}
