using System;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

[AttributeUsage(AttributeTargets.Property)]
public class FormControlSize : Attribute
{
    public SmartSoftwareFormControlSize Size { get; set; }

    public FormControlSize(SmartSoftwareFormControlSize size)
    {
        Size = size;
    }
}
