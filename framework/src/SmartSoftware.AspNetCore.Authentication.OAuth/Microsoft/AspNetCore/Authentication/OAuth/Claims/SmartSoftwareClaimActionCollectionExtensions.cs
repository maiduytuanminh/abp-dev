using SmartSoftware.AspNetCore.Authentication.OAuth.Claims;
using SmartSoftware.Security.Claims;

namespace Microsoft.AspNetCore.Authentication.OAuth.Claims;

public static class SmartSoftwareClaimActionCollectionExtensions
{
    public static void MapSmartSoftwareClaimTypes(this ClaimActionCollection claimActions)
    {
        if (SmartSoftwareClaimTypes.UserName != "name")
        {
            claimActions.MapJsonKey(SmartSoftwareClaimTypes.UserName, "name");
            claimActions.DeleteClaim("name");
            claimActions.RemoveDuplicate(SmartSoftwareClaimTypes.UserName);
        }
        
        if (SmartSoftwareClaimTypes.Name != "given_name")
        {
            claimActions.MapJsonKey(SmartSoftwareClaimTypes.Name, "given_name");
            claimActions.DeleteClaim("given_name");
            claimActions.RemoveDuplicate(SmartSoftwareClaimTypes.Name);
        }
                
        if (SmartSoftwareClaimTypes.SurName != "family_name")
        {
            claimActions.MapJsonKey(SmartSoftwareClaimTypes.SurName, "family_name");
            claimActions.DeleteClaim("family_name");
            claimActions.RemoveDuplicate(SmartSoftwareClaimTypes.SurName);
        }

        if (SmartSoftwareClaimTypes.Email != "email")
        {
            claimActions.MapJsonKey(SmartSoftwareClaimTypes.Email, "email");
            claimActions.DeleteClaim("email");
            claimActions.RemoveDuplicate(SmartSoftwareClaimTypes.Email);
        }

        if (SmartSoftwareClaimTypes.EmailVerified != "email_verified")
        {
            claimActions.MapJsonKey(SmartSoftwareClaimTypes.EmailVerified, "email_verified");
        }

        if (SmartSoftwareClaimTypes.PhoneNumber != "phone_number")
        {
            claimActions.MapJsonKey(SmartSoftwareClaimTypes.PhoneNumber, "phone_number");
        }

        if (SmartSoftwareClaimTypes.PhoneNumberVerified != "phone_number_verified")
        {
            claimActions.MapJsonKey(SmartSoftwareClaimTypes.PhoneNumberVerified, "phone_number_verified");
        }

        if (SmartSoftwareClaimTypes.Role != "role")
        {
            claimActions.MapJsonKeyMultiple(SmartSoftwareClaimTypes.Role, "role");
        }

        claimActions.RemoveDuplicate(SmartSoftwareClaimTypes.Name);
    }

    public static void MapJsonKeyMultiple(this ClaimActionCollection claimActions, string claimType, string jsonKey)
    {
        claimActions.Add(new MultipleClaimAction(claimType, jsonKey));
    }

    public static void RemoveDuplicate(this ClaimActionCollection claimActions, string claimType)
    {
        claimActions.Add(new RemoveDuplicateClaimAction(claimType));
    }
}
