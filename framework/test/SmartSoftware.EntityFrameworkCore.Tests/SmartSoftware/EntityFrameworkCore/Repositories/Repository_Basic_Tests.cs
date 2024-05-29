using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SmartSoftware.Data;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.TestApp;
using SmartSoftware.TestApp.Domain;
using SmartSoftware.TestApp.Testing;
using SmartSoftware.Uow;
using Xunit;

namespace SmartSoftware.EntityFrameworkCore.Repositories;

public class Repository_Basic_Tests : Repository_Basic_Tests<SmartSoftwareEntityFrameworkCoreTestModule>
{
    [Fact]
    public async Task EFCore_QueryableExtension_ToListAsync()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            var persons = await PersonRepository.ToListAsync();
            persons.Count.ShouldBeGreaterThan(0);
        });
    }

    [Fact]
    public async Task EFCore_QueryableExtension_CountAsync()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            var count = await PersonRepository.CountAsync();
            count.ShouldBeGreaterThan(0);
        });
    }

    [Fact]
    public async Task DeleteDirect_Test()
    {
        using (ServiceProvider.GetRequiredService<IUnitOfWorkManager>().Begin())
        {
            await PersonRepository.DeleteAsync(x => x.Id == TestDataBuilder.UserDouglasId);
            (await PersonRepository.GetDbContextAsync()).ChangeTracker.Entries<Person>().ShouldContain(x => x.Entity.Id == TestDataBuilder.UserDouglasId);
        }

        using (ServiceProvider.GetRequiredService<IUnitOfWorkManager>().Begin())
        {
            await PersonRepository.DeleteDirectAsync(x => x.Id == TestDataBuilder.UserDouglasId);
            (await PersonRepository.GetDbContextAsync()).ChangeTracker.Entries<Person>().ShouldNotContain(x => x.Entity.Id == TestDataBuilder.UserDouglasId);
        }
    }
}
