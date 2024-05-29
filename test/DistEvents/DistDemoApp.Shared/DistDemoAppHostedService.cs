using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SmartSoftware;

namespace DistDemoApp
{
    public class DistDemoAppHostedService : IHostedService
    {
        private readonly ISmartSoftwareApplicationWithExternalServiceProvider _application;
        private readonly IServiceProvider _serviceProvider;
        private readonly DemoService _demoService;

        public DistDemoAppHostedService(
            ISmartSoftwareApplicationWithExternalServiceProvider application,
            IServiceProvider serviceProvider,
            DemoService demoService)
        {
            _application = application;
            _serviceProvider = serviceProvider;
            _demoService = demoService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _application.Initialize(_serviceProvider);

            await _demoService.CreateTodoItemAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _application.Shutdown();

            return Task.CompletedTask;
        }
    }
}
