using SmartSoftware.UI.Navigation;

namespace SmartSoftware.UI.Navigation;

public interface IHasMenuItems
{
    /// <summary>
    /// Menu items.
    /// </summary>
    ApplicationMenuItemList Items { get; }
}
