using System;
using System.Threading.Tasks;
using SmartSoftware.Application.Services;

namespace SmartSoftware.CmsKit.Admin.Pages;

public interface IPageAdminAppService : ICrudAppService<PageDto, PageDto, Guid, GetPagesInputDto, CreatePageInputDto, UpdatePageInputDto>
{
    Task SetAsHomePageAsync(Guid id);
}
