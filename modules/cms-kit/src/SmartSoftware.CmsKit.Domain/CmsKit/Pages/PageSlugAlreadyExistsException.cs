using JetBrains.Annotations;
using SmartSoftware;

namespace SmartSoftware.CmsKit.Pages;

public class PageSlugAlreadyExistsException : BusinessException
{
    public PageSlugAlreadyExistsException([NotNull] string slug)
    {
        Code = CmsKitErrorCodes.Pages.SlugAlreadyExist;
        WithData(nameof(Page.Slug), slug);
    }
}
