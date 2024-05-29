using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;
using SmartSoftware.Validation;

namespace SmartSoftware.FluentValidation;

[DependsOn(
    typeof(SmartSoftwareValidationModule)
    )]
public class SmartSoftwareFluentValidationModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddConventionalRegistrar(new SmartSoftwareFluentValidationConventionalRegistrar());
    }
}
