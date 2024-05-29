using SmartSoftwarePerfTest.WithSmartSoftware.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartSoftware;
using SmartSoftware.AspNetCore.Mvc;
using SmartSoftware.Autofac;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore.SqlServer;
using SmartSoftware.Modularity;
using SmartSoftware.Uow;

namespace SmartSoftwarePerfTest.WithSmartSoftware
{
    [DependsOn(
        typeof(SmartSoftwareAspNetCoreMvcModule),
        typeof(SmartSoftwareAutofacModule),
        typeof(SmartSoftwareEntityFrameworkCoreSqlServerModule)
        )]
    public class AppModule : SmartSoftwareModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSmartSoftwareDbContext<BookDbContext>(options =>
            {
                options.AddDefaultRepositories();
            });

            Configure<SmartSoftwareDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

            Configure<SmartSoftwareUnitOfWorkDefaultOptions>(options =>
            {
                options.TransactionBehavior = UnitOfWorkTransactionBehavior.Auto;
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

            app.UseRouting();

            app.UseConfiguredEndpoints();
        }
    }
}
