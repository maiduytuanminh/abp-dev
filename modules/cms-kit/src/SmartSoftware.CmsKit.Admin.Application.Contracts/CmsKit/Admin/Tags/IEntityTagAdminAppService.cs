using System.Threading.Tasks;
using SmartSoftware.Application.Services;

namespace SmartSoftware.CmsKit.Admin.Tags;

public interface IEntityTagAdminAppService : IApplicationService
{
    Task AddTagToEntityAsync(EntityTagCreateDto input);

    Task RemoveTagFromEntityAsync(EntityTagRemoveDto input);

    Task SetEntityTagsAsync(EntityTagSetDto input);
}
