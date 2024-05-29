using System;
using System.Threading;
using System.Threading.Tasks;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.TestApp.Domain;
using SmartSoftware.TestApp.Testing;
using SmartSoftware.Uow;
using Xunit;

namespace SmartSoftware.MongoDB.Uow;

[Collection(MongoTestCollection.Name)]
public class UnitOfWork_CancellationToken_Tests : TestAppTestBase<SmartSoftwareMongoDbTestModule>
{
    [Fact]
    public async Task Should_Cancel_Test()
    {
        using (var uow = GetRequiredService<IUnitOfWorkManager>().Begin(isTransactional: true))
        {
            await Assert.ThrowsAsync<OperationCanceledException>(async () =>
            {
                var cst = new CancellationTokenSource();
                cst.Cancel();

                await GetRequiredService<IBasicRepository<Person, Guid>>().InsertAsync(new Person(Guid.NewGuid(), "Adam", 42));

                await uow.CompleteAsync(cst.Token);
            });
        }
    }
}
