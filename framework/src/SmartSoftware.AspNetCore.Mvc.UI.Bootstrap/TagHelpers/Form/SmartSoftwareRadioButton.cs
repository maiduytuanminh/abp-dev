using System;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

[AttributeUsage(AttributeTargets.Property)]
public class SmartSoftwareRadioButton : Attribute
{
    public bool Inline { get; set; } = false;

    public bool Disabled { get; set; } = false;
}
