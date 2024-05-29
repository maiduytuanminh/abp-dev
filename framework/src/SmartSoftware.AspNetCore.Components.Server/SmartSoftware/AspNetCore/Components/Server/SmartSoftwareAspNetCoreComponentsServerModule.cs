using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using SmartSoftware.AspNetCore.Auditing;
using SmartSoftware.AspNetCore.Components.Server.Extensibility;
using SmartSoftware.AspNetCore.Components.Web;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.SignalR;
using SmartSoftware.AspNetCore.Uow;
using SmartSoftware.EventBus;
using SmartSoftware.Http.Client;
using SmartSoftware.Modularity;

namespace SmartSoftware.AspNetCore.Components.Server;

[DependsOn(
    typeof(SmartSoftwareHttpClientModule),
    typeof(SmartSoftwareAspNetCoreComponentsWebModule),
    typeof(SmartSoftwareAspNetCoreSignalRModule),
    typeof(SmartSoftwareEventBusModule),
    typeof(SmartSoftwareAspNetCoreMvcContractsModule)
    )]
public class SmartSoftwareAspNetCoreComponentsServerModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        StaticWebAssetsLoader.UseStaticWebAssets(context.Services.GetHostingEnvironment(), context.Services.GetConfiguration());
        context.Services.AddHttpClient(nameof(BlazorServerLookupApiRequestService))
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.All
            });
        var serverSideBlazorBuilder = context.Services.AddServerSideBlazor(options =>
        {
            if (context.Services.GetHostingEnvironment().IsDevelopment())
            {
                options.DetailedErrors = true;
            }
        });
        context.Services.ExecutePreConfiguredActions(serverSideBlazorBuilder);

        Configure<SmartSoftwareAspNetCoreUnitOfWorkOptions>(options =>
        {
            options.IgnoredUrls.AddIfNotContains("/_blazor");
        });

        Configure<SmartSoftwareAspNetCoreAuditingOptions>(options =>
        {
            options.IgnoredUrls.AddIfNotContains("/_blazor");
        });

        if (!context.Services.ExecutePreConfiguredActions<SmartSoftwareAspNetCoreComponentsWebOptions>().IsBlazorWebApp)
        {
            var preConfigureActions = context.Services.GetPreConfigureActions<HttpConnectionDispatcherOptions>();
            Configure<SmartSoftwareEndpointRouterOptions>(options =>
            {
                options.EndpointConfigureActions.Add(endpointContext =>
                {
                    endpointContext.Endpoints.MapBlazorHub(httpConnectionDispatcherOptions =>
                    {
                        preConfigureActions.Configure(httpConnectionDispatcherOptions);
                    });
                    endpointContext.Endpoints.MapFallbackToPage("/_Host");
                });
            });
        }
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        context.GetEnvironment().WebRootFileProvider =
            new CompositeFileProvider(
                new ManifestEmbeddedFileProvider(typeof(IServerSideBlazorBuilder).Assembly),
                new ManifestEmbeddedFileProvider(typeof(RazorComponentsEndpointRouteBuilderExtensions).Assembly),
                context.GetEnvironment().WebRootFileProvider
            );
    }
}
