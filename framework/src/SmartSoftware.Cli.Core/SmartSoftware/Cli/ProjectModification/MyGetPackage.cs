using System.Collections.Generic;

namespace SmartSoftware.Cli.ProjectModification;

public class MyGetPackage
{
    public string PackageType { get; set; }

    public string Id { get; set; }

    public List<string> Versions { get; set; }

    public List<string> Dates { get; set; }
}
