using System.Threading.Tasks;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Application.Services;

namespace SmartSoftware.CmsKit.Public.Reactions;

public interface IReactionPublicAppService : IApplicationService
{
    Task<ListResultDto<ReactionWithSelectionDto>> GetForSelectionAsync(string entityType, string entityId);

    Task CreateAsync(string entityType, string entityId, string reaction);

    Task DeleteAsync(string entityType, string entityId, string reaction);
}
