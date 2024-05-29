using System;
using System.Threading.Tasks;

namespace SmartSoftware.BackgroundJobs.DemoApp.RabbitMq;

class Program
{
    async static Task Main(string[] args)
    {
        using (var application = await SmartSoftwareApplicationFactory.CreateAsync<DemoAppRabbitMqModule>(options =>
        {
            options.UseAutofac();
        }))
        {
            await application.InitializeAsync();

            Console.WriteLine("Started: " + typeof(Program).Namespace);
            Console.WriteLine("Press ENTER to stop the application..!");
            Console.ReadLine();

            await application.ShutdownAsync();
        }
    }
}
