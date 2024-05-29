using SmartSoftware.Collections;

namespace SmartSoftware.OpenIddict;

public class SmartSoftwareOpenIddictClaimsPrincipalOptions
{
    public ITypeList<ISmartSoftwareOpenIddictClaimsPrincipalHandler> ClaimsPrincipalHandlers { get; }

    public SmartSoftwareOpenIddictClaimsPrincipalOptions()
    {
        ClaimsPrincipalHandlers = new TypeList<ISmartSoftwareOpenIddictClaimsPrincipalHandler>();
    }
}
