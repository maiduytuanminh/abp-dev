using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace SmartSoftware.AspNetCore.Mvc.ApiExploring;

public class SmartSoftwareRemoteServiceApiDescriptionProviderOptions
{
    public HashSet<ApiResponseType> SupportedResponseTypes { get; set; } = new HashSet<ApiResponseType>();
}
