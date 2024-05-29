using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using SmartSoftware;
using SmartSoftware.Authorization;

namespace SmartSoftware.CmsKit.Admin;

public abstract class CmsKitAdminAppServiceBase : CmsKitAppServiceBase
{
    protected CmsKitAdminAppServiceBase()
    {
        ObjectMapperContext = typeof(CmsKitAdminApplicationModule);
    }

    /// <summary>
    /// Checks given policies until finding granted policy. If none of them is granted, throws <see cref="SmartSoftwareAuthorizationException"/>
    /// </summary>
    /// <param name="policies">Policies to be checked.</param>
    /// <exception cref="SmartSoftwareAuthorizationException">Thrown when none of policies is granted.</exception>
    protected async Task CheckAnyOfPoliciesAsync([NotNull] IEnumerable<string> policies)
    {
        Check.NotNull(policies, nameof(policies));

        foreach (var policy in policies)
        {
            if (await AuthorizationService.IsGrantedAsync(policy))
            {
                return;
            }
        }

        throw new SmartSoftwareAuthorizationException();
    }
}
