using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Modularity;

namespace SmartSoftware.Sms.Aliyun;

[DependsOn(typeof(SmartSoftwareSmsModule))]
public class SmartSoftwareSmsAliyunModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<SmartSoftwareAliyunSmsOptions>(configuration.GetSection("SmartSoftwareAliyunSms"));
    }
}
