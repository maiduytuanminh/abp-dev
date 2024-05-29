using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartSoftware.CmsKit.EntityFrameworkCore;
using SmartSoftware.CmsKit.MultiTenancy;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using SmartSoftware;
using SmartSoftware.AspNetCore.MultiTenancy;
using SmartSoftware.AspNetCore.Mvc.UI.MultiTenancy;
using SmartSoftware.AspNetCore.Mvc.UI.Theme.Shared;
using SmartSoftware.AspNetCore.Serilog;
using SmartSoftware.AuditLogging.EntityFrameworkCore;
using SmartSoftware.Autofac;
using SmartSoftware.BlobStoring.Database.EntityFrameworkCore;
using SmartSoftware.Caching;
using SmartSoftware.Caching.StackExchangeRedis;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.SqlServer;
using SmartSoftware.Localization;
using SmartSoftware.Modularity;
using SmartSoftware.MultiTenancy;
using SmartSoftware.PermissionManagement.EntityFrameworkCore;
using SmartSoftware.Security.Claims;
using SmartSoftware.SettingManagement.EntityFrameworkCore;
using SmartSoftware.VirtualFileSystem;

namespace SmartSoftware.CmsKit;

[DependsOn(
    typeof(CmsKitApplicationModule),
    typeof(CmsKitEntityFrameworkCoreModule),
    typeof(CmsKitHttpApiModule),
    typeof(SmartSoftwareAspNetCoreMvcUiMultiTenancyModule),
    typeof(SmartSoftwareAutofacModule),
    typeof(SmartSoftwareCachingStackExchangeRedisModule),
    typeof(SmartSoftwareEntityFrameworkCoreSqlServerModule),
    typeof(SmartSoftwareAuditLoggingEntityFrameworkCoreModule),
    typeof(SmartSoftwarePermissionManagementEntityFrameworkCoreModule),
    typeof(SmartSoftwareSettingManagementEntityFrameworkCoreModule),
    typeof(SmartSoftwareAspNetCoreSerilogModule),
    typeof(BlobStoringDatabaseEntityFrameworkCoreModule)
    )]
public class CmsKitHttpApiHostModule : SmartSoftwareModule
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

        Configure<SmartSoftwareMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<SmartSoftwareVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SmartSoftware.CmsKit.Domain.Shared", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SmartSoftware.CmsKit.Domain", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SmartSoftware.CmsKit.Application.Contracts", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<CmsKitApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}SmartSoftware.CmsKit.Application", Path.DirectorySeparatorChar)));
            });
        }

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

        context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.RequireHttpsMetadata = false;
                options.Audience = "CmsKit";
            });

        Configure<SmartSoftwareDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "CmsKit:";
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
        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }
        app.UseSmartSoftwareRequestLocalization();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");
        });
        app.UseAuditing();
        app.UseSmartSoftwareSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
