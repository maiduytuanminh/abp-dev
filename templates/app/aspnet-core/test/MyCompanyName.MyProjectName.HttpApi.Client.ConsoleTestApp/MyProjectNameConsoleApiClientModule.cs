using System;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using SmartSoftware.Autofac;
using SmartSoftware.Http.Client;
using SmartSoftware.Http.Client.IdentityModel;
using SmartSoftware.Modularity;

namespace MyCompanyName.MyProjectName.HttpApi.Client.ConsoleTestApp;

[DependsOn(
    typeof(SmartSoftwareAutofacModule),
    typeof(MyProjectNameHttpApiClientModule),
    typeof(SmartSoftwareHttpClientIdentityModelModule)
    )]
public class MyProjectNameConsoleApiClientModule : SmartSoftwareModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<SmartSoftwareHttpClientBuilderOptions>(options =>
        {
            options.ProxyClientBuildActions.Add((remoteServiceName, clientBuilder) =>
            {
                clientBuilder.AddTransientHttpErrorPolicy(
                    policyBuilder => policyBuilder.WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(Math.Pow(2, i)))
                );
            });
        });
    }
}
