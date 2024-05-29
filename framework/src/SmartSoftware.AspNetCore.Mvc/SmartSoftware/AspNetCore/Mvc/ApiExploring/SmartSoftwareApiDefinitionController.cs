using Microsoft.AspNetCore.Mvc;
using SmartSoftware.Http.Modeling;

namespace SmartSoftware.AspNetCore.Mvc.ApiExploring;

[Area("ss")]
[RemoteService(Name = "ss")]
[Route("api/ss/api-definition")]
public class SmartSoftwareApiDefinitionController : SmartSoftwareController, IRemoteService
{
    protected readonly IApiDescriptionModelProvider ModelProvider;

    public SmartSoftwareApiDefinitionController(IApiDescriptionModelProvider modelProvider)
    {
        ModelProvider = modelProvider;
    }

    [HttpGet]
    public virtual ApplicationApiDescriptionModel Get(ApplicationApiDescriptionModelRequestDto model)
    {
        return ModelProvider.CreateApiModel(model);
    }
}
