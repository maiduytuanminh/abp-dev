using Xunit;

namespace SmartSoftware.BackgroundJobs.MongoDB;

[Collection((MongoTestCollection.Name))]
public class BackgroundJobRepositoryTests : BackgroundJobRepository_Tests<SmartSoftwareBackgroundJobsMongoDbTestModule>
{

}
