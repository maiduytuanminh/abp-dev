using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using SmartSoftware.Cli.Args;
using SmartSoftware.DependencyInjection;

namespace SmartSoftware.Cli.Commands;

public class CommandSelector : ICommandSelector, ITransientDependency
{
    protected SmartSoftwareCliOptions Options { get; }

    public CommandSelector(IOptions<SmartSoftwareCliOptions> options)
    {
        Options = options.Value;
    }

    public Type Select(CommandLineArgs commandLineArgs)
    {
        if (commandLineArgs.Command.IsNullOrWhiteSpace())
        {
            return typeof(HelpCommand);
        }

        return Options.Commands.GetOrDefault(commandLineArgs.Command)
               ?? typeof(HelpCommand);
    }
}
