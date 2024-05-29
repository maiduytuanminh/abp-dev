using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Threading;

namespace SmartSoftware.BackgroundJobs.DemoApp.Shared.Jobs
{
    public class SampleJobCreator : ITransientDependency
    {
        private readonly IBackgroundJobManager _backgroundJobManager;

        public SampleJobCreator(IBackgroundJobManager backgroundJobManager)
        {
            _backgroundJobManager = backgroundJobManager;
        }

        public void CreateJobs()
        {
            AsyncHelper.RunSync(CreateJobsAsync);
        }

        public async Task CreateJobsAsync()
        {
            await _backgroundJobManager.EnqueueAsync(new WriteToConsoleGreenJobArgs { Value = "test 1 (green)" });
            await _backgroundJobManager.EnqueueAsync(new WriteToConsoleGreenJobArgs { Value = "test 2 (green)" });
            await _backgroundJobManager.EnqueueAsync(new WriteToConsoleYellowJobArgs { Value = "test 1 (yellow)" });
            await _backgroundJobManager.EnqueueAsync(new WriteToConsoleYellowJobArgs { Value = "test 2 (yellow)" });
        }
    }
}
