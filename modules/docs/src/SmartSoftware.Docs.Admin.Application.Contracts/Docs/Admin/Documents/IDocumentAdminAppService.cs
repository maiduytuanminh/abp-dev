using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.Application.Dtos;
using SmartSoftware.Application.Services;

namespace SmartSoftware.Docs.Admin.Documents
{
    public interface IDocumentAdminAppService : IApplicationService
    {
        Task ClearCacheAsync(ClearCacheInput input);

        Task PullAllAsync(PullAllDocumentInput input);

        Task PullAsync(PullDocumentInput input);

        Task<PagedResultDto<DocumentDto>> GetAllAsync(GetAllInput input);

        Task RemoveFromCacheAsync(Guid documentId);

        Task ReindexAsync(Guid documentId);

        Task<List<DocumentInfoDto>> GetFilterItemsAsync();
    }
}
