using JetBrains.Annotations;
using System.Threading.Tasks;
using SmartSoftware.Application.Services;
using SmartSoftware.CmsKit.Contents;

namespace SmartSoftware.CmsKit.Public.Pages;

public interface IPagePublicAppService : IApplicationService
{
    Task<PageDto> FindBySlugAsync([NotNull] string slug);
    Task<bool> DoesSlugExistAsync([NotNull] string slug);
    Task<PageDto> FindDefaultHomePageAsync();
}
