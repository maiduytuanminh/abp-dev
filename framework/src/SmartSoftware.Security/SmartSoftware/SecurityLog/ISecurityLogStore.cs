using System.Threading.Tasks;

namespace SmartSoftware.SecurityLog;

public interface ISecurityLogStore
{
    Task SaveAsync(SecurityLogInfo securityLogInfo);
}
