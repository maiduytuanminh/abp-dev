using SmartSoftware.Http;

namespace SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared.Views.Error;

public class SmartSoftwareErrorViewModel
{
    public RemoteServiceErrorInfo ErrorInfo { get; set; } = default!;

    public int HttpStatusCode { get; set; }
}
