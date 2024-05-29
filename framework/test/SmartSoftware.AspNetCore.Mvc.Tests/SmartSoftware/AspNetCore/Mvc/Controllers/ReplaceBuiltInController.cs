using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Controllers;
using SmartSoftware.AspNetCore.Mvc.ApplicationConfigurations;

namespace SmartSoftware.AspNetCore.Mvc.Controllers;

[Area("ss")]
[RemoteService(Name = "ss")]
[ReplaceControllers(typeof(SmartSoftwareApplicationConfigurationController), typeof(SmartSoftwareApplicationLocalizationController))]
public class ReplaceBuiltInController : SmartSoftwareController
{
    [HttpGet("api/ss/application-configuration")]
    public virtual Task<MyApplicationConfigurationDto> GetAsync(MyApplicationConfigurationRequestOptions options)
    {
        return Task.FromResult(new MyApplicationConfigurationDto()
        {
            Random = options.Random
        });
    }

    [HttpGet("api/ss/application-localization")]
    public virtual Task<MyApplicationLocalizationDto> GetAsync(MyApplicationLocalizationRequestDto input)
    {
        return Task.FromResult(new MyApplicationLocalizationDto()
        {
            Random = input.Random
        });
    }
}

public class MyApplicationConfigurationRequestOptions : ApplicationConfigurationRequestOptions
{
    public string Random { get; set; }
}

public class MyApplicationConfigurationDto : ApplicationConfigurationDto
{
    public string Random { get; set; }
}

public class MyApplicationLocalizationRequestDto : ApplicationLocalizationRequestDto
{
    public string Random { get; set; }
}

public class MyApplicationLocalizationDto : ApplicationLocalizationDto
{
    public string Random { get; set; }
}
