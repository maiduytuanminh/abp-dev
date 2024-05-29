using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace SmartSoftware.Localization;

public interface ISmartSoftwareStringLocalizer : IStringLocalizer
{
    IEnumerable<LocalizedString> GetAllStrings(
        bool includeParentCultures,
        bool includeBaseLocalizers,
        bool includeDynamicContributors
    );

    Task<IEnumerable<LocalizedString>> GetAllStringsAsync(
        bool includeParentCultures
    );
    
    Task<IEnumerable<LocalizedString>> GetAllStringsAsync(
        bool includeParentCultures,
        bool includeBaseLocalizers,
        bool includeDynamicContributors
    );

    Task<IEnumerable<string>> GetSupportedCulturesAsync();
}