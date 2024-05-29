using System;
using System.Threading.Tasks;

namespace SmartSoftware.Auditing;

public interface IAuditLogSaveHandle : IDisposable
{
    Task SaveAsync();
}
