using System.Threading.Tasks;
using SmartSoftware.Application.Services;

namespace SmartSoftware.TestApp.Application;

public interface IPeopleIntegrationService : IApplicationService
{
    Task<string> GetValueAsync();
}