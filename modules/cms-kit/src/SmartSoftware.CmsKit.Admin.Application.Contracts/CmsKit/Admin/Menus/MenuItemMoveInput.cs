using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSoftware.CmsKit.Admin.Menus;

[Serializable]
public class MenuItemMoveInput
{
    public Guid? NewParentId { get; set; }

    public int Position { get; set; }
}
