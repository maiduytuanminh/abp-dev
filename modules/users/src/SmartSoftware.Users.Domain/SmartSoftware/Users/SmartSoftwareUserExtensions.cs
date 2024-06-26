namespace SmartSoftware.Users;

public static class SmartSoftwareUserExtensions
{
    public static IUserData ToSmartSoftwareUserData(this IUser user)
    {
        return new UserData(
            id: user.Id,
            userName: user.UserName,
            email: user.Email,
            name: user.Name,
            surname: user.Surname,
            isActive: user.IsActive,
            emailConfirmed: user.EmailConfirmed,
            phoneNumber: user.PhoneNumber,
            phoneNumberConfirmed: user.PhoneNumberConfirmed,
            tenantId: user.TenantId,
            extraProperties: user.ExtraProperties
        );
    }
}
