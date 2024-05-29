namespace SmartSoftware.Identity;

public static class IdentityErrorCodes
{
    public const string UserSelfDeletion = "SmartSoftware.Identity:010001";
    public const string MaxAllowedOuMembership = "SmartSoftware.Identity:010002";
    public const string ExternalUserPasswordChange = "SmartSoftware.Identity:010003";
    public const string DuplicateOrganizationUnitDisplayName = "SmartSoftware.Identity:010004";
    public const string StaticRoleRenaming = "SmartSoftware.Identity:010005";
    public const string StaticRoleDeletion = "SmartSoftware.Identity:010006";
    public const string UsersCanNotChangeTwoFactor = "SmartSoftware.Identity:010007";
    public const string CanNotChangeTwoFactor = "SmartSoftware.Identity:010008";
    public const string YouCannotDelegateYourself = "SmartSoftware.Identity:010009";
    public const string ClaimNameExist = "SmartSoftware.Identity:010021";
}
