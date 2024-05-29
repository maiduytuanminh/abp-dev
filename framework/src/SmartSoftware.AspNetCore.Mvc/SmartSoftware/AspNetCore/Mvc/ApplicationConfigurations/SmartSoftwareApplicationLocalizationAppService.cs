using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using SmartSoftware.Application.Services;
using SmartSoftware.Localization;
using SmartSoftware.Localization.External;

namespace SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;

public class SmartSoftwareApplicationLocalizationAppService :
    ApplicationService,
    ISmartSoftwareApplicationLocalizationAppService
{
    protected IExternalLocalizationStore ExternalLocalizationStore { get; }
    protected SmartSoftwareLocalizationOptions LocalizationOptions { get; }

    public SmartSoftwareApplicationLocalizationAppService(
        IExternalLocalizationStore externalLocalizationStore,
        IOptions<SmartSoftwareLocalizationOptions> localizationOptions)
    {
        ExternalLocalizationStore = externalLocalizationStore;
        LocalizationOptions = localizationOptions.Value;
    }

    public virtual async Task<ApplicationLocalizationDto> GetAsync(ApplicationLocalizationRequestDto input)
    {
        if (!CultureHelper.IsValidCultureCode(input.CultureName))
        {
            throw new SmartSoftwareException("The selected culture is not valid! Make sure you enter a valid culture name.");
        }

        using (CultureHelper.Use(input.CultureName))
        {
            var resources = LocalizationOptions
                .Resources
                .Values
                .Union(
                    await ExternalLocalizationStore.GetResourcesAsync()
                ).ToArray();

            var localizationConfig = new ApplicationLocalizationDto
            {
                Resources = new Dictionary<string, ApplicationLocalizationResourceDto>(resources.Length),
                CurrentCulture = CurrentCultureDto.Create()
            };

            foreach (var resource in resources)
            {
                var dictionary = new Dictionary<string, string>();
                var localizer = await StringLocalizerFactory.CreateByResourceNameOrNullAsync(resource.ResourceName);
                if (localizer != null)
                {
                    Dictionary<string, LocalizedString>? staticLocalizedStrings = null;

                    if (input.OnlyDynamics)
                    {
                        staticLocalizedStrings = (await localizer.GetAllStringsAsync(
                            includeParentCultures: true,
                            includeBaseLocalizers: false,
                            includeDynamicContributors: false
                        )).ToDictionary(x => x.Name);
                    }

                    var localizedStringsWithDynamics = await localizer.GetAllStringsAsync(
                        includeParentCultures: true,
                        includeBaseLocalizers: false,
                        includeDynamicContributors: true
                    );

                    foreach (var localizedString in localizedStringsWithDynamics)
                    {
                        if (input.OnlyDynamics)
                        {
                            var staticLocalizedString = staticLocalizedStrings!.GetOrDefault(localizedString.Name);
                            if (staticLocalizedString != null &&
                                localizedString.Value == staticLocalizedString.Value)
                            {
                                continue;
                            }
                        }

                        dictionary[localizedString.Name] = localizedString.Value;
                    }
                }

                localizationConfig.Resources[resource.ResourceName] =
                    new ApplicationLocalizationResourceDto {
                        Texts = dictionary,
                        BaseResources = resource.BaseResourceNames.ToArray()
                    };
            }

            return localizationConfig;
        }
    }
}
