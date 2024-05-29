﻿using System;
using System.Threading.Tasks;
using SmartSoftware.AspNetCore.Components.Progression;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.BlazoriseUI;

[Dependency(ReplaceServices = true)]
public class BlazoriseUiPageProgressService : IUiPageProgressService,
    IScopedDependency
{
    /// <summary>
    /// An event raised after the notification is received.
    /// </summary>
    public event EventHandler<UiPageProgressEventArgs>? ProgressChanged;

    public Task Go(int? percentage, Action<UiPageProgressOptions>? options = null)
    {
        var uiPageProgressOptions = CreateDefaultOptions();
        options?.Invoke(uiPageProgressOptions);

        ProgressChanged?.Invoke(this, new UiPageProgressEventArgs(percentage, uiPageProgressOptions));

        return Task.CompletedTask;
    }

    protected virtual UiPageProgressOptions CreateDefaultOptions()
    {
        return new UiPageProgressOptions();
    }
}
