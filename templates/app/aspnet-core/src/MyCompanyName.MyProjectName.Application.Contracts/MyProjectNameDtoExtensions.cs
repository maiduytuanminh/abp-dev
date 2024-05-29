﻿using SmartSoftware.Identity;
using SmartSoftware.ObjectExtending;
using SmartSoftware.Threading;

namespace MyCompanyName.MyProjectName;

public static class MyProjectNameDtoExtensions
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        OneTimeRunner.Run(() =>
        {
                /* You can add extension properties to DTOs
                 * defined in the depended modules.
                 *
                 * Example:
                 *
                 * ObjectExtensionManager.Instance
                 *   .AddOrUpdateProperty<IdentityRoleDto, string>("Title");
                 *
                 * See the documentation for more:
                 * https://docs.smartsoftware.io/en/ss/latest/Object-Extensions
                 */
        });
    }
}