using System.Threading.Tasks;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Application.Services;
using SmartSoftware.Docs.Documents;

namespace SmartSoftware.Docs.Projects
{
    public interface IProjectAppService : IApplicationService
    {
        Task<ListResultDto<ProjectDto>> GetListAsync();

        Task<ProjectDto> GetAsync(string shortName);

        Task<ListResultDto<VersionInfoDto>> GetVersionsAsync(string shortName);

        Task<string> GetDefaultLanguageCodeAsync(string shortName, string version);

        Task<LanguageConfig> GetLanguageListAsync(string shortName, string version);
    }
}
