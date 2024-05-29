using System.Collections.Generic;

namespace SmartSoftware.CmsKit.Contents;

public interface IContent 
{
    List<ContentFragment> ContentFragments { get; set; }

    bool AllowHtmlTags { get; set; }
    
    bool PreventXSS { get; set; }

    string ReferralLink { get; set; }
}
