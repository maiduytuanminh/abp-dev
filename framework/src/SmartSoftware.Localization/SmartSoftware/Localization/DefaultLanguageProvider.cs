using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Localization;

public class DefaultLanguageProvider : ILanguageProvider, ITransientDependency
{
    protected SmartSoftwareLocalizationOptions Options { get; }

    public DefaultLanguageProvider(IOptions<SmartSoftwareLocalizationOptions> options)
    {
        Options = options.Value;
    }

    public Task<IReadOnlyList<LanguageInfo>> GetLanguagesAsync()
    {
        return Task.FromResult((IReadOnlyList<LanguageInfo>)Options.Languages);
    }
}
