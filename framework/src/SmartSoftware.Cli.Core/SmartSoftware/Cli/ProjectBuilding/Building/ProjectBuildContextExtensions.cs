﻿using System;
using System.Linq;
using SmartSoftware.Cli.ProjectBuilding.Files;

namespace SmartSoftware.Cli.ProjectBuilding.Building;

public static class ProjectBuildContextExtensions
{
    public static FileEntry GetFile(this ProjectBuildContext context, string filePath)
    {
        var file = context.Files.FirstOrDefault(f => f.Name == filePath);
        if (file == null)
        {
            throw new ApplicationException("Could not find file: " + filePath);
        }

        return file;
    }

    public static FileEntry FindFile(this ProjectBuildContext context, string filePath)
    {
        return context.Files.FirstOrDefault(f => f.Name == filePath);
    }
}
