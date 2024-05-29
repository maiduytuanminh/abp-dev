using System;

namespace SmartSoftware.Cli.Bundling;

internal class BundleTypeDefinition
{
    public int Level { get; set; }

    public Type BundleContributorType { get; set; }
}
