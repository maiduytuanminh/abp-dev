using System;
using System.Data;

namespace SmartSoftware.Uow;

public class SmartSoftwareUnitOfWorkOptions : ISmartSoftwareUnitOfWorkOptions
{
    /// <summary>
    /// Default: false.
    /// </summary>
    public bool IsTransactional { get; set; }

    public IsolationLevel? IsolationLevel { get; set; }

    /// <summary>
    /// Milliseconds
    /// </summary>
    public int? Timeout { get; set; }

    public SmartSoftwareUnitOfWorkOptions()
    {

    }

    public SmartSoftwareUnitOfWorkOptions(bool isTransactional = false, IsolationLevel? isolationLevel = null, int? timeout = null)
    {
        IsTransactional = isTransactional;
        IsolationLevel = isolationLevel;
        Timeout = timeout;
    }

    public SmartSoftwareUnitOfWorkOptions Clone()
    {
        return new SmartSoftwareUnitOfWorkOptions
        {
            IsTransactional = IsTransactional,
            IsolationLevel = IsolationLevel,
            Timeout = Timeout
        };
    }
}
