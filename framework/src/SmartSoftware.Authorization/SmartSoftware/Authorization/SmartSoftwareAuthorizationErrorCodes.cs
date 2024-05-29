namespace SmartSoftware.Authorization;

public static class SmartSoftwareAuthorizationErrorCodes
{
    public const string GivenPolicyHasNotGranted = "SmartSoftware.Authorization:010001";

    public const string GivenPolicyHasNotGrantedWithPolicyName = "SmartSoftware.Authorization:010002";

    public const string GivenPolicyHasNotGrantedForGivenResource = "SmartSoftware.Authorization:010003";

    public const string GivenRequirementHasNotGrantedForGivenResource = "SmartSoftware.Authorization:010004";

    public const string GivenRequirementsHasNotGrantedForGivenResource = "SmartSoftware.Authorization:010005";
}
