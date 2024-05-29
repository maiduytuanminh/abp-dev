using System.Threading.Tasks;
using JetBrains.Annotations;

namespace SmartSoftware.ExceptionHandling;

public interface IExceptionSubscriber
{
    Task HandleAsync([NotNull] ExceptionNotificationContext context);
}
