using System;
using SmartSoftware.Cli.Args;
using SmartSoftware.Cli.Utils;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Cli.ProjectModification;

public class AngularThemeConfigurer : ITransientDependency
{
    private readonly ICmdHelper _cmdHelper;

    public AngularThemeConfigurer(ICmdHelper cmdHelper)
    {
        _cmdHelper = cmdHelper;
    }

    public void Configure(AngularThemeConfigurationArgs args)
    {
        if (args.ProjectName.IsNullOrEmpty() || args.AngularFolderPath.IsNullOrEmpty())
        {
            return;
        }
        
        var command = "npx ng g @smartsoftware/ng.schematics:change-theme " +
                      $"--name {(int)args.Theme} " +
                      $"--target-project {args.ProjectName}";
        
        _cmdHelper.RunCmd(command, workingDirectory: args.AngularFolderPath);
    }
}