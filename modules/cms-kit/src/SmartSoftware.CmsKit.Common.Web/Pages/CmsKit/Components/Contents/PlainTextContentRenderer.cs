using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.CmsKit.Web.Pages.CmsKit.Components.Contents;

public class PlainTextContentRenderer : IContentRenderer, ITransientDependency
{
    public Task<string> RenderAsync(string value)
    {
        return Task.FromResult(value);
    }
}
