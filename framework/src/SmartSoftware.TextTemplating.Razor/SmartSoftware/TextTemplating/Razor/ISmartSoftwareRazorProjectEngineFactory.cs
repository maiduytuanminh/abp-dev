using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language;

namespace SmartSoftware.TextTemplating.Razor;

public interface ISmartSoftwareRazorProjectEngineFactory
{
    Task<RazorProjectEngine> CreateAsync(Action<RazorProjectEngineBuilder>? configure = null);
}
