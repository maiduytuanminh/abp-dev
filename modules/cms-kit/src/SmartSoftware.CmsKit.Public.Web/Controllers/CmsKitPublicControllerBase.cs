using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.CmsKit.Localization;

namespace SmartSoftware.CmsKit.Public.Web.Controllers;

public abstract class CmsKitPublicControllerBase : SmartSoftwareController
{
	public CmsKitPublicControllerBase()
	{
		LocalizationResource = typeof(CmsKitResource);
	}
}
