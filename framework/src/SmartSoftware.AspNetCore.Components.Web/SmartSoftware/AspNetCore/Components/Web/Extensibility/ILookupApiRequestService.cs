using System.Threading.Tasks;
using JetBrains.Annotations;

namespace SmartSoftware.AspNetCore.Components.Web.Extensibility;

public interface ILookupApiRequestService
{
    Task<string> SendAsync([NotNull] string url);
}
