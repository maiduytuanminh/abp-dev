using System;
using System.Threading.Tasks;

namespace SmartSoftware.SecurityLog;

public interface ISecurityLogManager
{
    Task SaveAsync(Action<SecurityLogInfo>? saveAction = null);
}
