﻿using MyCompanyName.MyProjectName.Localization;
using SmartSoftware.AspNetCore.Mvc.UI.RazorPages;

namespace MyCompanyName.MyProjectName.Web.Pages;

public abstract class MyProjectNamePageModel : SmartSoftwarePageModel
{
    protected MyProjectNamePageModel()
    {
        LocalizationResourceType = typeof(MyProjectNameResource);
    }
}
