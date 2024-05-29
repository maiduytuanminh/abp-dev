﻿namespace SmartSoftware.Tracing;

public class SmartSoftwareCorrelationIdOptions
{
    public string HttpHeaderName { get; set; } = "X-Correlation-Id";

    public bool SetResponseHeader { get; set; } = true;
}
