using System.Threading.Tasks;
using SmartSoftware;
using SmartSoftware.Options;

namespace Microsoft.Extensions.Options;

public static class OptionsSmartSoftwareDynamicOptionsManagerExtensions
{
    public static Task SetAsync<T>(this IOptions<T> options)
        where T : class
    {
        return options.ToDynamicOptions().SetAsync();
    }

    public static Task SetAsync<T>(this IOptions<T> options, string name)
        where T : class
    {
        return options.ToDynamicOptions().SetAsync(name);
    }

    private static SmartSoftwareDynamicOptionsManager<T> ToDynamicOptions<T>(this IOptions<T> options)
        where T : class
    {
        if (options is SmartSoftwareDynamicOptionsManager<T> dynamicOptionsManager)
        {
            return dynamicOptionsManager;
        }

        throw new SmartSoftwareException($"Options must be derived from the {typeof(SmartSoftwareDynamicOptionsManager<>).FullName}!");
    }
}
