using Microsoft.Extensions.DependencyInjection;
using SmartSoftware.Authorization.Permissions;
using SmartSoftware.Authorization.TestServices;
using SmartSoftware.Autofac;
using SmartSoftware.DynamicProxy;
using SmartSoftware.ExceptionHandling;
using SmartSoftware.Modularity;

namespace SmartSoftware.Authorization;

[DependsOn(typeof(SmartSoftwareAutofacModule))]
[DependsOn(typeof(SmartSoftwareAuthorizationModule))]
[DependsOn(typeof(SmartSoftwareExceptionHandlingModule))]
public class SmartSoftwareAuthorizationTestModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.OnRegistered(onServiceRegistredContext =>
        {
            if (typeof(IMyAuthorizedService1).IsAssignableFrom(onServiceRegistredContext.ImplementationType) &&
                !DynamicProxyIgnoreTypes.Contains(onServiceRegistredContext.ImplementationType))
            {
                onServiceRegistredContext.Interceptors.TryAdd<AuthorizationInterceptor>();
            }
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<SmartSoftwarePermissionOptions>(options =>
        {
            options.ValueProviders.Add<TestPermissionValueProvider1>();
            options.ValueProviders.Add<TestPermissionValueProvider2>();
        });
    }
}
