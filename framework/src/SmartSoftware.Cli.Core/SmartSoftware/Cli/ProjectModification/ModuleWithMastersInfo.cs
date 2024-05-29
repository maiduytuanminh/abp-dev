using System.Collections.Generic;

namespace SmartSoftware.Cli.ProjectModification;

public class ModuleWithMastersInfo : ModuleInfo
{
    public List<ModuleWithMastersInfo> MasterModuleInfos { get; set; }
}
