using System.Collections.Generic;
using SmartSoftware;
using SmartSoftware.CmsKit.Web.Icons;

namespace SmartSoftware.CmsKit.Web.Reactions;

public class ReactionIconDictionary : Dictionary<string, LocalizableIconDictionary>
{
    public string GetLocalizedIcon(string name, string cultureName = null)
    {
        var icon = this.GetOrDefault(name);
        if (icon == null)
        {
            throw new SmartSoftwareException($"No icon defined for the reaction with name '{name}'");
        }

        return icon.GetLocalizedIconOrDefault(cultureName);
    }
}
