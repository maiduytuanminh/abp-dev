using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.TestBase;
using SmartSoftware.Modularity;

namespace SmartSoftware.Identity.AspNetCore;

[DependsOn(
    typeof(SmartSoftwareIdentityAspNetCoreModule),
    typeof(SmartSoftwareIdentityDomainTestModule),
    typeof(SmartSoftwareAspNetCoreTestBaseModule),
    typeof(SmartSoftwareAspNetCoreMvcModule)
)]
public class SmartSoftwareIdentityAspNetCoreTestModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<IMvcBuilder>(builder =>
        {
            builder.PartManager.ApplicationParts.Add(new AssemblyPart(typeof(SmartSoftwareIdentityAspNetCoreTestModule).Assembly));
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwareIdentityOptions>(options =>
        {
            options.ExternalLoginProviders.Add<FakeExternalLoginProvider>(FakeExternalLoginProvider.Name);
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseConfiguredEndpoints();
    }
}
