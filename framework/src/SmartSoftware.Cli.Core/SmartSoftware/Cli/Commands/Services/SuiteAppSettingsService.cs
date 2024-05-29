﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SmartSoftware.Cli.Utils;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Cli.Commands.Services;

public class SuiteAppSettingsService : ITransientDependency
{
    private const int DefaultPort = 3000;
    
    public CmdHelper CmdHelper { get; }

    public SuiteAppSettingsService(CmdHelper cmdHelper)
    {
        CmdHelper = cmdHelper;
    }
    
    public async Task<int> GetSuitePortAsync()
    {
        return await GetSuitePortAsync(GetCurrentSuiteVersion());
    }
    
    public async Task<int> GetSuitePortAsync(string version)
    {
        var filePath = GetFilePathOrNull(version);

        if (filePath == null)
        {
            return DefaultPort;
        }

        var content = File.ReadAllText(filePath);

        var contentAsJson = JObject.Parse(content);

        var url = contentAsJson["SmartSoftwareSuite"]?["ApplicationUrl"]?.ToString();

        if (url == null)
        {
            return DefaultPort;
        }
        
        return Convert.ToInt32(url.Split(":").Last());
    }
    
    public int GetSuitePort()
    {
        return GetSuitePort(GetCurrentSuiteVersion());
    }
    
    public int GetSuitePort(string version)
    {
        var filePath = GetFilePathOrNull(version);

        if (filePath == null)
        {
            return DefaultPort;
        }

        var content = File.ReadAllText(filePath);

        var contentAsJson = JObject.Parse(content);

        var url = contentAsJson["SmartSoftwareSuite"]?["ApplicationUrl"]?.ToString();

        if (url == null)
        {
            return DefaultPort;
        }
        
        return Convert.ToInt32(url.Split(":").Last());
    }

    private string GetFilePathOrNull(string version)
    {
        if (version == null)
        {
            return null;
        }
        
        var path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".dotnet",
            "tools",
            ".store",
            "smartsoftware.suite",
            version,
            "smartsoftware.suite",
            version,
            "tools",
            "net8.0",
            "any",
            "appsettings.json"
            );

        if (!File.Exists(path))
        {
            return null;
        }

        return path;
    }

    private string GetCurrentSuiteVersion()
    {
        var dotnetToolList = CmdHelper.RunCmdAndGetOutput("dotnet tool list -g", out int exitCode);

        var suiteLine = dotnetToolList.Split(Environment.NewLine)
            .FirstOrDefault(l => l.ToLower().StartsWith("smartsoftware.suite "));

        if (string.IsNullOrEmpty(suiteLine))
        {
            return null;
        }

        return suiteLine.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1];
    }
}
