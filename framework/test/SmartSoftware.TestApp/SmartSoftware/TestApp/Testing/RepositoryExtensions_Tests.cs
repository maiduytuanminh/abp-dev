using System;
using System.Threading.Tasks;
using SmartSoftware.Domain.Entities;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.Modularity;
using SmartSoftware.TestApp.Domain;
using Xunit;

namespace SmartSoftware.TestApp.Testing;

public abstract class RepositoryExtensions_Tests<TStartupModule> : TestAppTestBase<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    protected readonly IRepository<Person, Guid> PersonRepository;

    protected RepositoryExtensions_Tests()
    {
        PersonRepository = GetRequiredService<IRepository<Person, Guid>>();
    }

    [Fact]
    public async Task EnsureExistsAsync_Test()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            var id = Guid.NewGuid();
            await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
                await PersonRepository.EnsureExistsAsync(Guid.NewGuid())
            );
            await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
                await PersonRepository.EnsureExistsAsync(x => x.Id == id)
            );

            await PersonRepository.EnsureExistsAsync(TestDataBuilder.UserDouglasId);
            await PersonRepository.EnsureExistsAsync(x => x.Id == TestDataBuilder.UserDouglasId);
        });
    }
}
