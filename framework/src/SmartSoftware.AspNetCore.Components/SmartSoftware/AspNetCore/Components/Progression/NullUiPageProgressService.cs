using System;
using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Components.Progression;

public class NullUiPageProgressService : IUiPageProgressService, ISingletonDependency
{
    public event EventHandler<UiPageProgressEventArgs> ProgressChanged = default!;

    public Task Go(int? percentage, Action<UiPageProgressOptions>? options = null)
    {
        return Task.CompletedTask;
    }
}
