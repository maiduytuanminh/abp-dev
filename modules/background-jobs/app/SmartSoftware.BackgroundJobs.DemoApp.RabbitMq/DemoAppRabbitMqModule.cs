using SmartSoftware.Autofac;
using SmartSoftware.BackgroundJobs.DemoApp.Shared;
using SmartSoftware.BackgroundJobs.RabbitMQ;
using SmartSoftware.Modularity;

namespace SmartSoftware.BackgroundJobs.DemoApp.RabbitMq;

[DependsOn(
    typeof(DemoAppSharedModule),
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareBackgroundJobsRabbitMqModule)
)]
public class DemoAppRabbitMqModule : SmartSoftwareModule
{

}
