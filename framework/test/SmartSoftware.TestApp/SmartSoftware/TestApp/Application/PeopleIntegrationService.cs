using System.Threading.Tasks;
using SmartSoftware.Application.Services;

namespace SmartSoftware.TestApp.Application;

[IntegrationService]
public class PeopleIntegrationService : ApplicationService, IPeopleIntegrationService
{
    public async Task<string> GetValueAsync()
    {
        return "42";
    }
}