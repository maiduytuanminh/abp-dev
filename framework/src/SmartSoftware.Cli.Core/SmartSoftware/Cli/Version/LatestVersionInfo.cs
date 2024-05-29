﻿using NuGet.Versioning;

namespace SmartSoftware.Cli.Version;

public class LatestVersionInfo
{
    public SemanticVersion Version { get; }

    public string Message { get; }

    public LatestVersionInfo(SemanticVersion version, string message = null)
    {
        Version = version;
        Message = message;
    }
}