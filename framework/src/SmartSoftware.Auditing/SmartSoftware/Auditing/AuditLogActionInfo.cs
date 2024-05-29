﻿using System;
using System.Collections.Generic;
using SmartSoftware.Data;

namespace SmartSoftware.Auditing;

[Serializable]
public class AuditLogActionInfo : IHasExtraProperties
{
    public string ServiceName { get; set; } = default!;

    public string MethodName { get; set; } = default!;

    public string Parameters { get; set; } = default!;

    public DateTime ExecutionTime { get; set; }

    public int ExecutionDuration { get; set; }

    public ExtraPropertyDictionary ExtraProperties { get; }

    public AuditLogActionInfo()
    {
        ExtraProperties = new ExtraPropertyDictionary();
    }
}
