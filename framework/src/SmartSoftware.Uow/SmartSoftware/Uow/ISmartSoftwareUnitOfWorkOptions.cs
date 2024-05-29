using System;
using System.Data;

namespace SmartSoftware.Uow;

public interface ISmartSoftwareUnitOfWorkOptions
{
    bool IsTransactional { get; }

    IsolationLevel? IsolationLevel { get; }

    /// <summary>
    /// Milliseconds
    /// </summary>
    int? Timeout { get; }
}
