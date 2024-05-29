using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.AspNetCore.Components.MauiBlazor;

public class NullMauiBlazorSelectedLanguageProvider : IMauiBlazorSelectedLanguageProvider, ITransientDependency
{
    public Task<string?> GetSelectedLanguageAsync()
    {
        return Task.FromResult((string?)null);
    }
}