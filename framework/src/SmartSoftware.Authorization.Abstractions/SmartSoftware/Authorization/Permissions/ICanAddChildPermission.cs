using JetBrains.Annotations;
using SmartSoftware.Localization;
using SmartSoftware.MultiTenancy;

namespace SmartSoftware.Authorization.Permissions;

public interface ICanAddChildPermission
{
    PermissionDefinition AddPermission(
        [NotNull] string name,
        ILocalizableString? displayName = null,
        MultiTenancySides multiTenancySide = MultiTenancySides.Both,
        bool isEnabled = true);
}