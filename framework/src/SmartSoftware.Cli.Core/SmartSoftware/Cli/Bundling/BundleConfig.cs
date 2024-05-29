﻿using SmartSoftware.Bundling;

namespace SmartSoftware.Cli.Bundling;

public class BundleConfig
{
    public bool IsBlazorWebApp { get; set; } = false;

    public bool InteractiveAuto { get; set; } = false;

    public BundlingMode Mode { get; set; } = BundlingMode.BundleAndMinify;

    public string Name { get; set; } = "global";

    public BundleParameterDictionary Parameters { get; set; } = new();
}
