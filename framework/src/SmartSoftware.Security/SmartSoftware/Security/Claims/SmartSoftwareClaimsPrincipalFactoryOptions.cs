using System.Collections.Generic;
using System.Security.Claims;
using SmartSoftware.Collections;

namespace SmartSoftware.Security.Claims;

public class SmartSoftwareClaimsPrincipalFactoryOptions
{
    public ITypeList<ISmartSoftwareClaimsPrincipalContributor> Contributors { get; }

    public ITypeList<ISmartSoftwareDynamicClaimsPrincipalContributor> DynamicContributors { get; }

    public List<string> DynamicClaims { get; }

    public bool IsRemoteRefreshEnabled { get; set; }

    public string RemoteRefreshUrl { get; set; }

    public Dictionary<string, List<string>> ClaimsMap { get; set; }

    public bool IsDynamicClaimsEnabled { get; set; }

    public SmartSoftwareClaimsPrincipalFactoryOptions()
    {
        Contributors = new TypeList<ISmartSoftwareClaimsPrincipalContributor>();
        DynamicContributors = new TypeList<ISmartSoftwareDynamicClaimsPrincipalContributor>();

        DynamicClaims = new List<string>
        {
            SmartSoftwareClaimTypes.UserName,
            SmartSoftwareClaimTypes.Name,
            SmartSoftwareClaimTypes.SurName,
            SmartSoftwareClaimTypes.Role,
            SmartSoftwareClaimTypes.Email,
            SmartSoftwareClaimTypes.EmailVerified,
            SmartSoftwareClaimTypes.PhoneNumber,
            SmartSoftwareClaimTypes.PhoneNumberVerified
        };

        RemoteRefreshUrl = "/api/account/dynamic-claims/refresh";
        IsRemoteRefreshEnabled = true;

        ClaimsMap = new Dictionary<string, List<string>>()
        {
            { SmartSoftwareClaimTypes.UserName, new List<string> { "preferred_username", "unique_name", ClaimTypes.Name }},
            { SmartSoftwareClaimTypes.Name, new List<string> { "given_name", ClaimTypes.GivenName }},
            { SmartSoftwareClaimTypes.SurName, new List<string> { "family_name", ClaimTypes.Surname }},
            { SmartSoftwareClaimTypes.Role, new List<string> { "role", "roles", ClaimTypes.Role }},
            { SmartSoftwareClaimTypes.Email, new List<string> { "email", ClaimTypes.Email }},
        };

        IsDynamicClaimsEnabled = false;
    }
}
