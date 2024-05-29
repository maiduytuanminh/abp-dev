using Microsoft.AspNetCore.Identity;
using static SmartSoftware.Identity.AspNetCore.SmartSoftwareSecurityStampValidatorCallback;

namespace SmartSoftware.Identity.AspNetCore;

public static class SecurityStampValidatorOptionsExtensions
{
    public static SecurityStampValidatorOptions UpdatePrincipal(this SecurityStampValidatorOptions options, SmartSoftwareRefreshingPrincipalOptions ssRefreshingPrincipalOptions)
    {
        var previousOnRefreshingPrincipal = options.OnRefreshingPrincipal;
        options.OnRefreshingPrincipal = async context =>
        {
            await SecurityStampValidatorCallback.UpdatePrincipal(context, ssRefreshingPrincipalOptions);
            if(previousOnRefreshingPrincipal != null)
            {
                await previousOnRefreshingPrincipal.Invoke(context);
            }
        };
        return options;
    }
}
