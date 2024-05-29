using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using SmartSoftware.Options;

namespace Microsoft.AspNetCore.RequestLocalization;

public class SmartSoftwareRequestLocalizationOptionsManager : SmartSoftwareDynamicOptionsManager<RequestLocalizationOptions>
{
    private RequestLocalizationOptions? _options;

    private readonly ISmartSoftwareRequestLocalizationOptionsProvider _ssRequestLocalizationOptionsProvider;

    public SmartSoftwareRequestLocalizationOptionsManager(
        IOptionsFactory<RequestLocalizationOptions> factory,
        ISmartSoftwareRequestLocalizationOptionsProvider ssRequestLocalizationOptionsProvider)
        : base(factory)
    {
        _ssRequestLocalizationOptionsProvider = ssRequestLocalizationOptionsProvider;
    }

    public override RequestLocalizationOptions Get(string? name)
    {
        return _options ?? base.Get(name);
    }

    protected override async Task OverrideOptionsAsync(string name, RequestLocalizationOptions options)
    {
        _options = await _ssRequestLocalizationOptionsProvider.GetLocalizationOptionsAsync();
    }
}
