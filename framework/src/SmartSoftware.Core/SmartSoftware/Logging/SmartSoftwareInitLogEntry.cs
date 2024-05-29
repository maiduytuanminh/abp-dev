﻿using System;
using Microsoft.Extensions.Logging;

namespace SmartSoftware.Logging;

public class SmartSoftwareInitLogEntry
{
    public LogLevel LogLevel { get; set; }

    public EventId EventId { get; set; }

    public object State { get; set; } = default!;

    public Exception? Exception { get; set; }

    public Func<object, Exception?, string> Formatter { get; set; } = default!;

    public string Message => Formatter(State, Exception);
}
