using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Shouldly;
using SmartSoftware.TestApp.Domain;
using SmartSoftware.TestApp.Testing;
using SmartSoftware.Timing;
using Xunit;

namespace SmartSoftware.MongoDB.Serializer;

[Collection(MongoTestCollection.Name)]
public abstract class MongoDB_DateTimeKind_Tests : DateTimeKind_Tests<SmartSoftwareMongoDbTestModule>
{
    protected override void AfterAddApplication(IServiceCollection services)
    {
        // MongoDB uses static properties to store the mapping information,
        // We must reconfigure it in the new unit test.
        foreach (var registeredClassMap in BsonClassMap.GetRegisteredClassMaps())
        {
            foreach (var declaredMemberMap in registeredClassMap.DeclaredMemberMaps)
            {
                var serializer = declaredMemberMap.GetSerializer();
                switch (serializer)
                {
                    case SmartSoftwareMongoDbDateTimeSerializer dateTimeSerializer:
                        dateTimeSerializer.SetDateTimeKind(Kind);
                        break;
                    case NullableSerializer<DateTime> nullableSerializer:
                        {
                            var lazySerializer = nullableSerializer.GetType()
                                ?.GetField("_lazySerializer", BindingFlags.NonPublic | BindingFlags.Instance)
                                ?.GetValue(serializer)?.As<Lazy<IBsonSerializer<DateTime>>>();

                            if (lazySerializer?.Value is SmartSoftwareMongoDbDateTimeSerializer dateTimeSerializer)
                            {
                                dateTimeSerializer.SetDateTimeKind(Kind);
                            }
                            break;
                        }
                }
            }
        }
    }
}

public class DateTimeKindTests_Unspecified : MongoDB_DateTimeKind_Tests
{
    protected override void AfterAddApplication(IServiceCollection services)
    {
        Kind = DateTimeKind.Unspecified;
        services.Configure<SmartSoftwareClockOptions>(x => x.Kind = Kind);
        base.AfterAddApplication(services);
    }
}

[Collection(MongoTestCollection.Name)]
public class DateTimeKindTests_Local : MongoDB_DateTimeKind_Tests
{
    protected override void AfterAddApplication(IServiceCollection services)
    {
        Kind = DateTimeKind.Local;
        services.Configure<SmartSoftwareClockOptions>(x => x.Kind = Kind);
        base.AfterAddApplication(services);
    }
}

[Collection(MongoTestCollection.Name)]
public class DateTimeKindTests_Utc : MongoDB_DateTimeKind_Tests
{
    protected override void AfterAddApplication(IServiceCollection services)
    {
        Kind = DateTimeKind.Utc;
        services.Configure<SmartSoftwareClockOptions>(x => x.Kind = Kind);
        base.AfterAddApplication(services);
    }
}

[Collection(MongoTestCollection.Name)]
public class DisableDateTimeKindTests : TestAppTestBase<SmartSoftwareMongoDbTestModule>
{
    protected IPersonRepository PersonRepository { get; }

    public DisableDateTimeKindTests()
    {
        PersonRepository = GetRequiredService<IPersonRepository>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.Configure<SmartSoftwareMongoDbOptions>(x => x.UseSmartSoftwareClockHandleDateTime = false);
        base.AfterAddApplication(services);
    }

    [Fact]
    public async Task DateTime_Kind_Should_Be_Normalized_By_MongoDb_Test()
    {
        var personId = Guid.NewGuid();
        await PersonRepository.InsertAsync(new Person(personId, "bob lee", 18)
        {
            Birthday = DateTime.Parse("2020-01-01 00:00:00"),
            LastActive = DateTime.Parse("2020-01-01 00:00:00"),
        }, true);

        var person = await PersonRepository.GetAsync(personId);

        person.ShouldNotBeNull();
        person.CreationTime.Kind.ShouldBe(DateTimeKind.Utc);

        person.Birthday.ShouldNotBeNull();
        person.Birthday.Value.Kind.ShouldBe(DateTimeKind.Utc);
    }
}
