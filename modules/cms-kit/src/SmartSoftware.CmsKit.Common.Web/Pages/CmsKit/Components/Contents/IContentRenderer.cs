using System.Threading.Tasks;

namespace SmartSoftware.CmsKit.Web.Pages.CmsKit.Components.Contents;

public interface IContentRenderer
{
    Task<string> RenderAsync(string value);
}
