using IdentityModel;
using SmartSoftware.AspNetCore.Components.WebAssembly;
using SmartSoftware.Modularity;
using SmartSoftware.Security.Claims;

namespace SmartSoftware.Http.Client.IdentityModel.WebAssembly;

[DependsOn(
    typeof(SmartSoftwareHttpClientIdentityModelModule),
    typeof(SmartSoftwareAspNetCoreComponentsWebAssemblyModule)
)]
public class SmartSoftwareHttpClientIdentityModelWebAssemblyModule : SmartSoftwareModule
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
