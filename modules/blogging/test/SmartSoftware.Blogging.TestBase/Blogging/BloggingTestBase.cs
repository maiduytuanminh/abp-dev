using System;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using SmartSoftware;
using SmartSoftware.Modularity;
using SmartSoftware.Users;
using SmartSoftware.Testing;

namespace SmartSoftware.Blogging
{
    public abstract class BloggingTestBase<TStartupModule> : SmartSoftwareIntegratedTest<TStartupModule>
        where TStartupModule : ISmartSoftwareModule
    {
        protected Guid? CurrentUserId { get; set; }

        protected BloggingTestBase()
        {
            CurrentUserId = Guid.NewGuid();
        }

        protected override void SetSmartSoftwareApplicationCreationOptions(SmartSoftwareApplicationCreationOptions options)
        {
            options.UseAutofac();
        }

        protected override void AfterAddApplication(IServiceCollection services)
        {
            var currentUser = Substitute.For<ICurrentUser>();
            currentUser.Id.Returns(ci => CurrentUserId);
            services.AddSingleton(currentUser);
        }
    }
}
