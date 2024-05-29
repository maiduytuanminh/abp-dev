using System.Threading.Tasks;
using JetBrains.Annotations;
using SmartSoftware;

namespace Microsoft.Extensions.Localization;

public static class SmartSoftwareStringLocalizerFactoryExtensions
{
    public static IStringLocalizer? CreateDefaultOrNull(this IStringLocalizerFactory localizerFactory)
    {
        return (localizerFactory as ISmartSoftwareStringLocalizerFactory)
            ?.CreateDefaultOrNull();
    }

    public static IStringLocalizer? CreateByResourceNameOrNull(
        this IStringLocalizerFactory localizerFactory,
        string resourceName)
    {
        return (localizerFactory as ISmartSoftwareStringLocalizerFactory)
            ?.CreateByResourceNameOrNull(resourceName);
    }
    
    [NotNull]
    public static IStringLocalizer CreateByResourceName(
        this IStringLocalizerFactory localizerFactory,
        string resourceName)
    {
        var localizer = localizerFactory.CreateByResourceNameOrNull(resourceName);
        if (localizer == null)
        {
            throw new SmartSoftwareException("Couldn't find a localizer with given resource name: " + resourceName);
        }
        
        return localizer;
    }
    
    public static async Task<IStringLocalizer?> CreateByResourceNameOrNullAsync(
        this IStringLocalizerFactory localizerFactory,
        string resourceName)
    {
        var ssLocalizerFactory = localizerFactory as ISmartSoftwareStringLocalizerFactory;
        if (ssLocalizerFactory == null)
        {
            return null;
        } 
        
        return await ssLocalizerFactory.CreateByResourceNameOrNullAsync(resourceName);
    }
    
    [NotNull]
    public async static Task<IStringLocalizer> CreateByResourceNameAsync(
        this IStringLocalizerFactory localizerFactory,
        string resourceName)
    {
        var localizer = await localizerFactory.CreateByResourceNameOrNullAsync(resourceName);
        if (localizer == null)
        {
            throw new SmartSoftwareException("Couldn't find a localizer with given resource name: " + resourceName);
        }
        
        return localizer;
    }
}
