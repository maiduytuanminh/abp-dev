using System.Collections.Generic;
using SmartSoftware.Modularity;
using SmartSoftware.CmsKit.Comments;

namespace SmartSoftware.CmsKit;

[DependsOn(
    typeof(CmsKitApplicationModule),
    typeof(CmsKitDomainTestModule)
    )]
public class CmsKitApplicationTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<CmsKitCommentOptions>(options =>
        {
            options.AllowedExternalUrls = new Dictionary<string, List<string>>
            {
                {
                    "EntityName1",
                    new List<string>
                    {
                        "https://smartsoftware.io/"
                    }
                }
            };
        });
    }
}
