﻿using Blazorise;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace SmartSoftware.BlazoriseUI.Components;

public partial class ToolbarButton : ComponentBase
{
    [Parameter]
    public Color? Color { get; set; }

    [Parameter]
    public object? Icon { get; set; }

    [Parameter]
    public string Text { get; set; } = default!;

    [Parameter]
    public Func<Task> Clicked { get; set; } = default!;

    [Parameter]
    public bool Disabled { get; set; }
}
