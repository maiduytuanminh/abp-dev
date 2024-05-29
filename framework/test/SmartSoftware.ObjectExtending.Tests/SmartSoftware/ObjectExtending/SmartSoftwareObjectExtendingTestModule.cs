using SmartSoftware.Modularity;
using SmartSoftware.ObjectExtending.TestObjects;
using SmartSoftware.Threading;

namespace SmartSoftware.ObjectExtending;

[DependsOn(
    typeof(SmartSoftwareObjectExtendingModule),
    typeof(SmartSoftwareTestBaseModule)
    )]
public class SmartSoftwareObjectExtendingTestModule : SmartSoftwareModule
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        OneTimeRunner.Run(() =>
        {
            ObjectExtensionManager.Instance
                .AddOrUpdateProperty<ExtensibleTestPerson, string>("Name")
                .AddOrUpdateProperty<ExtensibleTestPerson, int>("Age")
                .AddOrUpdateProperty<ExtensibleTestPerson, string>("NoPairCheck", options => options.CheckPairDefinitionOnMapping = false)
                .AddOrUpdateProperty<ExtensibleTestPerson, string>("CityName")
                .AddOrUpdateProperty<ExtensibleTestPerson, ExtensibleTestEnumProperty>("EnumProperty")
                .AddOrUpdateProperty<ExtensibleTestPersonDto, string>("Name")
                .AddOrUpdateProperty<ExtensibleTestPersonDto, int>("ChildCount")
                .AddOrUpdateProperty<ExtensibleTestPersonDto, string>("CityName")
                .AddOrUpdateProperty<ExtensibleTestPersonDto, ExtensibleTestEnumProperty>("EnumProperty")
                .AddOrUpdateProperty<ExtensibleTestPersonWithRegularPropertiesDto, string>("Name")
                .AddOrUpdateProperty<ExtensibleTestPersonWithRegularPropertiesDto, int>("Age");
        });
    }
}
