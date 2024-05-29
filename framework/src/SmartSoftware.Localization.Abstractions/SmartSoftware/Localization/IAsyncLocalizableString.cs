using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace SmartSoftware.Localization;

public interface IAsyncLocalizableString
{
    Task<LocalizedString> LocalizeAsync(IStringLocalizerFactory stringLocalizerFactory);
}