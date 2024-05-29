using System.Threading.Tasks;

namespace SmartSoftware.AspNetCore.Components.MauiBlazor;

public interface IMauiBlazorSelectedLanguageProvider
{
    Task<string?> GetSelectedLanguageAsync();
}