﻿namespace SmartSoftware.AspNetCore.Mvc.UI.Bundling.TagHelpers;

public interface IBundleItemTagHelper : IBundleTagHelper
{
    BundleTagHelperItem CreateBundleTagHelperItem();
}
