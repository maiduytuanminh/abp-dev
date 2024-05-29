﻿using SmartSoftware.Reflection;

namespace SmartSoftware.SettingManagement;

public class SettingManagementPermissions
{
    public const string GroupName = "SettingManagement";

    public const string Emailing = GroupName + ".Emailing";

    public const string EmailingTest = Emailing + ".Test";

    public const string TimeZone = GroupName + ".TimeZone";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(SettingManagementPermissions));
    }
}
