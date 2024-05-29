using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.DependencyInjection;
using SmartSoftware.ObjectMapping;

namespace SmartSoftware.BackgroundJobs;

public class BackgroundJobStore : IBackgroundJobStore, ITransientDependency
{
    protected IBackgroundJobRepository BackgroundJobRepository { get; }

    protected IObjectMapper<SmartSoftwareBackgroundJobsDomainModule> ObjectMapper { get; }

    public BackgroundJobStore(
        IBackgroundJobRepository backgroundJobRepository,
        IObjectMapper<SmartSoftwareBackgroundJobsDomainModule> objectMapper)
    {
        ObjectMapper = objectMapper;
        BackgroundJobRepository = backgroundJobRepository;
    }

    public virtual async Task<BackgroundJobInfo> FindAsync(Guid jobId)
    {
        return ObjectMapper.Map<BackgroundJobRecord, BackgroundJobInfo>(
            await BackgroundJobRepository.FindAsync(jobId)
        );
    }

    public virtual async Task InsertAsync(BackgroundJobInfo jobInfo)
    {
        await BackgroundJobRepository.InsertAsync(
            ObjectMapper.Map<BackgroundJobInfo, BackgroundJobRecord>(jobInfo)
        );
    }

    public virtual async Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(int maxResultCount)
    {
        return ObjectMapper.Map<List<BackgroundJobRecord>, List<BackgroundJobInfo>>(
            await BackgroundJobRepository.GetWaitingListAsync(maxResultCount)
        );
    }

    public virtual async Task DeleteAsync(Guid jobId)
    {
        await BackgroundJobRepository.DeleteAsync(jobId);
    }

    public virtual async Task UpdateAsync(BackgroundJobInfo jobInfo)
    {
        var backgroundJobRecord = await BackgroundJobRepository.FindAsync(jobInfo.Id);
        if (backgroundJobRecord == null)
        {
            return;
        }

        ObjectMapper.Map(jobInfo, backgroundJobRecord);
        await BackgroundJobRepository.UpdateAsync(backgroundJobRecord);
    }
}
