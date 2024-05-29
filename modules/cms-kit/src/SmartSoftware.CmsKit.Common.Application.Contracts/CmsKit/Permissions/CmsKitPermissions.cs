using SmartSoftware.Reflection;

namespace SmartSoftware.CmsKit.Permissions;

public class CmsKitPermissions
{
    public const string GroupName = "CmsKit.Public";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CmsKitPermissions));
    }
}
