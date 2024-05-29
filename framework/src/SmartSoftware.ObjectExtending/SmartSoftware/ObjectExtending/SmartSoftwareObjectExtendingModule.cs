using SmartSoftware.Modularity;
using SmartSoftware.Validation;
using SmartSoftware.Localization;

namespace SmartSoftware.ObjectExtending;

[DependsOn(
    typeof(SmartSoftwareLocalizationAbstractionsModule),
    typeof(SmartSoftwareValidationAbstractionsModule)
    )]
public class SmartSoftwareObjectExtendingModule : SmartSoftwareModule
{

}
