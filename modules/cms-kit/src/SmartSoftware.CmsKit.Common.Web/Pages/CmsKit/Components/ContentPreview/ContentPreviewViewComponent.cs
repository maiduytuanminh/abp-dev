using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.CmsKit.Contents;
using SmartSoftware.CmsKit.Web.Contents;

namespace SmartSoftware.CmsKit.Web.Pages.CmsKit.Components.ContentPreview;

public class ContentPreviewViewComponent : SmartSoftwareViewComponent
{
    protected ContentParser ContentParser { get; }

    public ContentPreviewViewComponent(ContentParser contentParser)
    {
        ContentParser = contentParser;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync(string content)
    {
        var fragments = await ContentParser.ParseAsync(content);

        return View("~/Pages/CmsKit/Components/ContentPreview/Default.cshtml", new DefaultContentDto
        {
            ContentFragments = fragments,
            AllowHtmlTags = true,
            PreventXSS = false
        });
    }
}
