using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using SmartSoftware.Domain.Repositories.MongoDB;
using SmartSoftware.MongoDB;
using SmartSoftware.TestApp.Domain;

namespace SmartSoftware.TestApp.MongoDB;

public class PersonRepository : MongoDbRepository<ITestAppMongoDbContext, Person, Guid>, IPersonRepository
{
    public PersonRepository(IMongoDbContextProvider<ITestAppMongoDbContext> dbContextProvider)
        : base(dbContextProvider)
    {

    }

    public async Task<PersonView> GetViewAsync(string name)
    {
        var person = await (await (await GetCollectionAsync()).FindAsync(x => x.Name == name)).FirstOrDefaultAsync();
        return new PersonView()
        {
            Name = person.Name,
            CreationTime = person.CreationTime,
            Birthday = person.Birthday,
            LastActive = person.LastActive
        };
    }
}
