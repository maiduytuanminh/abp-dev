using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartSoftware.Localization;

public interface ILanguageProvider
{
    Task<IReadOnlyList<LanguageInfo>> GetLanguagesAsync();
}
