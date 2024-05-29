using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSoftware.CmsKit.MediaDescriptors;

public class CmsKitMediaOptions
{
    [NotNull]
    public List<MediaDescriptorDefinition> EntityTypes { get; } = new();
}
