using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace SmartSoftware.Logging;

public interface IInitLogger<out T> : ILogger<T>
{
    public List<SmartSoftwareInitLogEntry> Entries { get; }
}
