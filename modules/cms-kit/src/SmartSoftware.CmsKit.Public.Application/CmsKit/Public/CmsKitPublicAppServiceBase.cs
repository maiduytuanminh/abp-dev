namespace SmartSoftware.CmsKit.Public;

public abstract class CmsKitPublicAppServiceBase : CmsKitAppServiceBase
{
    protected CmsKitPublicAppServiceBase()
    {
        ObjectMapperContext = typeof(CmsKitPublicApplicationModule);
    }
}
