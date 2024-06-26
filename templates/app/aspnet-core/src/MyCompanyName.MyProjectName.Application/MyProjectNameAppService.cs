﻿using System;
using System.Collections.Generic;
using System.Text;
using MyCompanyName.MyProjectName.Localization;
using SmartSoftware.Application.Services;

namespace MyCompanyName.MyProjectName;

/* Inherit your application services from this class.
 */
public abstract class MyProjectNameAppService : ApplicationService
{
    protected MyProjectNameAppService()
    {
        LocalizationResource = typeof(MyProjectNameResource);
    }
}
