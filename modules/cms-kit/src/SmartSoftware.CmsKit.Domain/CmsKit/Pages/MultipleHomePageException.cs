using SmartSoftware;

namespace SmartSoftware.CmsKit.Domain.SmartSoftware.CmsKit.Pages;

public class MultipleHomePageException : BusinessException
{
	public MultipleHomePageException()
	{
		Code = CmsKitErrorCodes.Pages.MultipleHomePage;
	}
}
