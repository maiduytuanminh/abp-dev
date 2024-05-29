using System.Threading.Tasks;
using JetBrains.Annotations;

namespace SmartSoftware.ExceptionHandling;

public interface IExceptionNotifier
{
    Task NotifyAsync([NotNull] ExceptionNotificationContext context);
}
