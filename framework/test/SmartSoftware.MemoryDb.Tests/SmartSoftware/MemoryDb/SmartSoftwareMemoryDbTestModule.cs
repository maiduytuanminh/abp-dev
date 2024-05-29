using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmartSoftware.Modularity;
using SmartSoftware.TestApp.MemoryDb;
using SmartSoftware.Data;
using SmartSoftware.Autofac;
using SmartSoftware.Domain.Repositories.MemoryDb;
using SmartSoftware.Json.SystemTextJson;
using SmartSoftware.MemoryDb.JsonConverters;
using SmartSoftware.TestApp;
using SmartSoftware.TestApp.Domain;

namespace SmartSoftware.MemoryDb;

[DependsOn(
    typeof(TestAppModule),
    typeof(SmartSoftwareMemoryDbModule),
    typeof(SmartSoftwareAutofacModule))]
public class SmartSoftwareMemoryDbTestModule : SmartSoftwareModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var connStr = Guid.NewGuid().ToString();

        Configure<SmartSoftwareDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = connStr;
        });

        context.Services.AddMemoryDbContext<TestAppMemoryDbContext>(options =>
        {
            options.AddDefaultRepositories();
            options.AddRepository<City, CityRepository>();
        });

        context.Services.AddOptions<Utf8JsonMemoryDbSerializerOptions>()
            .Configure<IServiceProvider>((options, rootServiceProvider) =>
            {
                options.JsonSerializerOptions.Converters.Add(new EntityJsonConverter<EntityWithIntPk, int>());
                options.JsonSerializerOptions.TypeInfoResolver = new SmartSoftwareDefaultJsonTypeInfoResolver(rootServiceProvider
                    .GetRequiredService<IOptions<SmartSoftwareSystemTextJsonSerializerModifiersOptions>>());
            });
    }
}
