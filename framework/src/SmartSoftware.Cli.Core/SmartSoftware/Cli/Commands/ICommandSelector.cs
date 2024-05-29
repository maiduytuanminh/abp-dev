using System;
using SmartSoftware.Cli.Args;

namespace SmartSoftware.Cli.Commands;

public interface ICommandSelector
{
    Type Select(CommandLineArgs commandLineArgs);
}
