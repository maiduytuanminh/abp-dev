using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSoftware.Data;
using SmartSoftware.Domain.Entities;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.Modularity;
using SmartSoftware.TestApp.Domain;
using SmartSoftware.Uow;
using Xunit;

namespace SmartSoftware.TestApp.Testing;

public abstract class HardDelete_Tests<TStartupModule> : TestAppTestBase<TStartupModule>
    where TStartupModule : ISmartSoftwareModule
{
    protected readonly IRepository<Person, Guid> PersonRepository;
    protected readonly IDataFilter DataFilter;
    protected readonly IUnitOfWorkManager UnitOfWorkManager;

    protected HardDelete_Tests()
    {
        PersonRepository = GetRequiredService<IRepository<Person, Guid>>();
        DataFilter = GetRequiredService<IDataFilter>();
        UnitOfWorkManager = GetRequiredService<IUnitOfWorkManager>();
    }

    [Fact]
    public async Task Should_HardDelete_Entities()
    {
        var douglas = await PersonRepository.GetAsync(TestDataBuilder.UserDouglasId);
        await PersonRepository.HardDeleteAsync(douglas);

        using (DataFilter.Disable<ISoftDelete>())
        {
            douglas = await PersonRepository.FindAsync(TestDataBuilder.UserDouglasId);
            douglas.ShouldBeNull();
        }
    }

    [Fact]
    public async Task Should_HardDelete_Entities_With_Predicate()
    {
        await PersonRepository.HardDeleteAsync(x => x.Id == TestDataBuilder.UserDouglasId || x.Id == TestDataBuilder.UserJohnDeletedId);

        using (DataFilter.Disable<ISoftDelete>())
        {
            var douglas = await PersonRepository.FindAsync(TestDataBuilder.UserDouglasId);
            douglas.ShouldBeNull();

            var john = await PersonRepository.FindAsync(TestDataBuilder.UserJohnDeletedId);
            john.ShouldBeNull();
        }
    }

    [Fact]
    public async Task Should_HardDelete_Entities_With_IEnumerable()
    {
        using (DataFilter.Disable<ISoftDelete>())
        {
            var persons = await PersonRepository.GetListAsync();
            await PersonRepository.HardDeleteAsync(persons);

            var personCount = await PersonRepository.GetCountAsync();
            personCount.ShouldBe(0);
        }
    }

    [Fact]
    public async Task Should_HardDelete_Soft_Deleted_Entities()
    {
        var douglas = await PersonRepository.GetAsync(TestDataBuilder.UserDouglasId);
        await PersonRepository.DeleteAsync(douglas);

        douglas = await PersonRepository.FindAsync(TestDataBuilder.UserDouglasId);
        douglas.ShouldBeNull();

        using (DataFilter.Disable<ISoftDelete>())
        {
            douglas = await PersonRepository.FindAsync(TestDataBuilder.UserDouglasId);
            douglas.ShouldNotBeNull();
            douglas.IsDeleted.ShouldBeTrue();
            douglas.DeletionTime.ShouldNotBeNull();
        }

        using (var uow = UnitOfWorkManager.Begin())
        {
            using (DataFilter.Disable<ISoftDelete>())
            {
                douglas = await PersonRepository.GetAsync(TestDataBuilder.UserDouglasId);
            }

            await PersonRepository.HardDeleteAsync(douglas);
            await uow.CompleteAsync();
        }

        using (DataFilter.Disable<ISoftDelete>())
        {
            douglas = await PersonRepository.FindAsync(TestDataBuilder.UserDouglasId);
            douglas.ShouldBeNull();
        }
    }

    [Fact]
    public async Task Should_HardDelete_Soft_Deleted_Entities_With_Predicate()
    {
        var douglas = await PersonRepository.GetAsync(TestDataBuilder.UserDouglasId);
        await PersonRepository.DeleteAsync(douglas);

        douglas = await PersonRepository.FindAsync(TestDataBuilder.UserDouglasId);
        douglas.ShouldBeNull();

        using (DataFilter.Disable<ISoftDelete>())
        {
            douglas = await PersonRepository.FindAsync(TestDataBuilder.UserDouglasId);
            douglas.ShouldNotBeNull();
            douglas.IsDeleted.ShouldBeTrue();
            douglas.DeletionTime.ShouldNotBeNull();
        }

        using (var uow = UnitOfWorkManager.Begin())
        {
            await PersonRepository.HardDeleteAsync(x => x.Id == TestDataBuilder.UserDouglasId || x.Id == TestDataBuilder.UserJohnDeletedId);
            await uow.CompleteAsync();
        }

        using (DataFilter.Disable<ISoftDelete>())
        {
            douglas = await PersonRepository.FindAsync(TestDataBuilder.UserDouglasId);
            douglas.ShouldBeNull();

            var john = await PersonRepository.FindAsync(TestDataBuilder.UserJohnDeletedId);
            john.ShouldBeNull();
        }
    }

    [Fact]
    public async Task Should_HardDelete_WithDeleteMany()
    {
        var persons = await PersonRepository.GetListAsync();

        await WithUnitOfWorkAsync(async () =>
        {
            var hardDeleteEntities = (HashSet<IEntity>)UnitOfWorkManager.Current.Items.GetOrAdd(
                  UnitOfWorkItemNames.HardDeletedEntities,
                  () => new HashSet<IEntity>()
                  );
            hardDeleteEntities.UnionWith(persons);
            await PersonRepository.DeleteManyAsync(persons);
        });

        var personsCount = await PersonRepository.GetCountAsync();

        personsCount.ShouldBe(0);
    }

    [Fact]
    public async Task Should_HardDelete_WithDeleteMany_WithPredicate()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            await PersonRepository.HardDeleteAsync(x => x.Id == TestDataBuilder.UserDouglasId);

            await PersonRepository.DeleteManyAsync(new[] { TestDataBuilder.UserDouglasId });
        });

        var douglas = await PersonRepository.FindAsync(TestDataBuilder.UserDouglasId);

        douglas.ShouldBeNull();
    }
}
