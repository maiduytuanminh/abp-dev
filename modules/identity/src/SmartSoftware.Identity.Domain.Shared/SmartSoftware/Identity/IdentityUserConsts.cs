using SmartSoftware.Users;

namespace SmartSoftware.Identity;

public static class IdentityUserConsts
{
    public static int MaxUserNameLength { get; set; } = SmartSoftwareUserConsts.MaxUserNameLength;

    public static int MaxNameLength { get; set; } = SmartSoftwareUserConsts.MaxNameLength;

    public static int MaxSurnameLength { get; set; } = SmartSoftwareUserConsts.MaxSurnameLength;

    public static int MaxNormalizedUserNameLength { get; set; } = MaxUserNameLength;

    public static int MaxEmailLength { get; set; } = SmartSoftwareUserConsts.MaxEmailLength;

    public static int MaxNormalizedEmailLength { get; set; } = MaxEmailLength;

    public static int MaxPhoneNumberLength { get; set; } = SmartSoftwareUserConsts.MaxPhoneNumberLength;

    /// <summary>
    /// Default value: 128
    /// </summary>
    public static int MaxPasswordLength { get; set; } = 128;

    /// <summary>
    /// Default value: 256
    /// </summary>
    public static int MaxPasswordHashLength { get; set; } = 256;

    /// <summary>
    /// Default value: 256
    /// </summary>
    public static int MaxSecurityStampLength { get; set; } = 256;

    /// <summary>
    /// Default value: 16
    /// </summary>
    public static int MaxLoginProviderLength { get; set; } = 16;
}
