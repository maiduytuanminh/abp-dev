using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartSoftware.CmsKit.MultiTenancy;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using SmartSoftware;
using SmartSoftware.Account;
using SmartSoftware.Account.Web;
using SmartSoftware.AspNetCore.Authentication.JwtBearer;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.AspNetCore.Mvc.UI.MultiTenancy;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Basic;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;
using SmartSoftware.AspNetCore.Serilog;
using SmartSoftware.Auditing;
using SmartSoftware.AuditLogging.EntityFrameworkCore;
using SmartSoftware.Autofac;
using SmartSoftware.Caching;
using SmartSoftware.Caching.StackExchangeRedis;
using SmartSoftware.Data;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.SqlServer;
using SmartSoftware.FeatureManagement;
using SmartSoftware.Identity;
using SmartSoftware.Identity.EntityFrameworkCore;
using SmartSoftware.IdentityServer.EntityFrameworkCore;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.PermissionManagement;
using SmartSoftware.PermissionManagement.EntityFrameworkCore;
using SmartSoftware.PermissionManagement.HttpApi;
using SmartSoftware.PermissionManagement.Identity;
using SmartSoftware.SettingManagement.EntityFrameworkCore;
using SmartSoftware.TenantManagement;
using SmartSoftware.TenantManagement.EntityFrameworkCore;
using SmartSoftware.Threading;
using SmartSoftware.UI.Navigation.Urls;

namespace SmartSoftware.CmsKit;

[DependsOn(
    typeof(SmartSoftwareAccountWebIdentityServerModule),
    typeof(SmartSoftwareAccountApplicationModule),
    typeof(SmartSoftwareAccountHttpApiModule),
    typeof(SmartSoftwareAspNetCoreMvcUiMultiTenancyModule),
    typeof(SmartSoftwareAspNetCoreMvcModule),
    typeof(SmartSoftwareAspNetCoreMvcUiBasicThemeModule),
    typeof(SmartSoftwareAuditLoggingEntityFrameworkCoreModule),
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareCachingStackExchangeRedisModule),
    typeof(SmartSoftwareEntityFrameworkCoreSqlServerModule),
    typeof(SmartSoftwareIdentityEntityFrameworkCoreModule),
    typeof(SmartSoftwareIdentityApplicationModule),
    typeof(SmartSoftwareIdentityHttpApiModule),
    typeof(SmartSoftwareIdentityServerEntityFrameworkCoreModule),
    typeof(SmartSoftwarePermissionManagementDomainIdentityModule),
    typeof(SmartSoftwarePermissionManagementEntityFrameworkCoreModule),
    typeof(SmartSoftwarePermissionManagementApplicationModule),
    typeof(SmartSoftwarePermissionManagementHttpApiModule),
    typeof(SmartSoftwareSettingManagementEntityFrameworkCoreModule),
    typeof(SmartSoftwareFeatureManagementApplicationModule),
    typeof(SmartSoftwareTenantManagementEntityFrameworkCoreModule),
    typeof(SmartSoftwareTenantManagementApplicationModule),
    typeof(SmartSoftwareTenantManagementHttpApiModule),
    typeof(SmartSoftwareAspNetCoreAuthenticationJwtBearerModule),
    typeof(CmsKitApplicationContractsModule),
    typeof(SmartSoftwareAspNetCoreSerilogModule)
    )]
public class CmsKitIdentityServerModule : SmartSoftwareModule
{
    private const string DefaultCorsPolicyName = "Default";

    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        FeatureConfigurer.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        Configure<SmartSoftwareDbContextOptions>(options =>
        {
            options.UseSqlServer();
        });

        context.Services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "CmsKit API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });

        Configure<SmartSoftwareLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi"));
            options.Languages.Add(new LanguageInfo("is", "is", "Icelandic"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italiano"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
            options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
            options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            options.Languages.Add(new LanguageInfo("el", "el", "Ελληνικά"));
        });

        Configure<SmartSoftwareAuditingOptions>(options =>
        {
                //options.IsEnabledForGetRequests = true;
                options.ApplicationName = "AuthServer";
        });

        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
        });

        context.Services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.RequireHttpsMetadata = false;
                options.Audience = configuration["AuthServer:ApiName"];
            });

        Configure<SmartSoftwareDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "CmsKit:";
        });

        Configure<SmartSoftwareMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });

        if (!hostingEnvironment.IsDevelopment())
        {
            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            context.Services
                .AddDataProtection()
                .PersistKeysToStackExchangeRedis(redis, "CmsKit-Protection-Keys");
        }

        context.Services.AddCors(options =>
        {
            options.AddPolicy(DefaultCorsPolicyName, builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray()
                    )
                    .WithSmartSoftwareExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseErrorPage();
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors(DefaultCorsPolicyName);
        app.UseAuthentication();
        app.UseJwtTokenMiddleware();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseSmartSoftwareRequestLocalization();
        app.UseIdentityServer();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");
        });
        app.UseAuditing();
        app.UseSmartSoftwareSerilogEnrichers();
        app.UseConfiguredEndpoints();

        SeedData(context);
    }

    private void SeedData(ApplicationInitializationContext context)
    {
        AsyncHelper.RunSync(async () =>
        {
            using (var scope = context.ServiceProvider.CreateScope())
            {
                await scope.ServiceProvider
                    .GetRequiredService<IDataSeeder>()
                    .SeedAsync();
            }
        });
    }
}
