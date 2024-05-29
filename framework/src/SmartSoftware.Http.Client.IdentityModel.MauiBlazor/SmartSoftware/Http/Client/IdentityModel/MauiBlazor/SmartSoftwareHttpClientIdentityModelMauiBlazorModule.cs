using IdentityModel;
using SmartSoftware.AspNetCore.Components.MauiBlazor;
using SmartSoftware.Modularity;
using SmartSoftware.Security.Claims;

namespace SmartSoftware.Http.Client.IdentityModel.MauiBlazor;

[DependsOn(
    typeof(SmartSoftwareHttpClientIdentityModelModule),
    typeof(SmartSoftwareAspNetCoreComponentsMauiBlazorModule)
)]
public class SmartSoftwareHttpClientIdentityModelMauiBlazorModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        SmartSoftwareClaimTypes.UserName = JwtClaimTypes.PreferredUserName;
        SmartSoftwareClaimTypes.Name = JwtClaimTypes.GivenName;
        SmartSoftwareClaimTypes.SurName = JwtClaimTypes.FamilyName;
        SmartSoftwareClaimTypes.UserId = JwtClaimTypes.Subject;
        SmartSoftwareClaimTypes.Role = JwtClaimTypes.Role;
        SmartSoftwareClaimTypes.Email = JwtClaimTypes.Email;
    }
}
