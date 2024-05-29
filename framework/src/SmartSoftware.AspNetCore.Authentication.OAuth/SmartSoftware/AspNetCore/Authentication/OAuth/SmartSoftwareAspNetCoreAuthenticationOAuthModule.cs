using SmartSoftware.Modularity;
using SmartSoftware.Security;

namespace SmartSoftware.AspNetCore.Authentication.OAuth;

[DependsOn(typeof(SmartSoftwareSecurityModule))]
public class SmartSoftwareAspNetCoreAuthenticationOAuthModule : SmartSoftwareModule
{

}
