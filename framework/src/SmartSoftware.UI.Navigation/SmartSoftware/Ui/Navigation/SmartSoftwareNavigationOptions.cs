using System.Collections.Generic;
using JetBrains.Annotations;

namespace SmartSoftware.UI.Navigation;

public class SmartSoftwareNavigationOptions
{
    [NotNull]
    public List<IMenuContributor> MenuContributors { get; }

    /// <summary>
    /// Includes the <see cref="StandardMenus.Main"/> by default.
    /// </summary>
    public List<string> MainMenuNames { get; }

    public SmartSoftwareNavigationOptions()
    {
        MenuContributors = new List<IMenuContributor>();

        MainMenuNames = new List<string>
            {
                StandardMenus.Main
            };
    }
}
