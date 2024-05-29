using Localization.Resources.SmartSoftwareUi;
using SmartSoftware.Account.Localization;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Identity;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace SmartSoftware.Account;

[DependsOn(
    typeof(SmartSoftwareAccountApplicationContractsModule),
    typeof(SmartSoftwareIdentityHttpApiModule),
    typeof(SmartSoftwareAspNetCoreMvcModule))]
public class SmartSoftwareAccountHttpApiModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartSoftwareAccountHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Resources
                .Get<AccountResource>()
                .AddBaseTypes(typeof(SmartSoftwareUiResource));
        });
    }
}
