using System;

namespace SmartSoftware.Tracing;

public interface ICorrelationIdProvider
{
    string? Get();

    IDisposable Change(string? correlationId);
}
