using System;

namespace SmartSoftware.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Modal;

[Flags]
public enum SmartSoftwareModalButtons
{
    None = 0,
    Save = 1,
    Cancel = 2,
    Close = 4
}
