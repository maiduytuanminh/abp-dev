using System;
using System.Collections.Generic;
using SmartSoftware.Security.Claims;

namespace SmartSoftware.AspNetCore.Security.Claims;

public class SmartSoftwareClaimsMapOptions
{
    public Dictionary<string, Func<string>> Maps { get; }

    public SmartSoftwareClaimsMapOptions()
    {
        Maps = new Dictionary<string, Func<string>>()
            {
                { "sub", () => SmartSoftwareClaimTypes.UserId },
                { "role", () => SmartSoftwareClaimTypes.Role },
                { "email", () => SmartSoftwareClaimTypes.Email },
                { "name", () => SmartSoftwareClaimTypes.UserName },
                { "family_name", () => SmartSoftwareClaimTypes.SurName },
                { "given_name", () => SmartSoftwareClaimTypes.Name }
            };
    }
}
