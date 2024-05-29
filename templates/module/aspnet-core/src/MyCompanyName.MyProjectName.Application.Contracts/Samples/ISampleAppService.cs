using System.Threading.Tasks;
using SmartSoftware.Application.Services;

namespace MyCompanyName.MyProjectName.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
