using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSoftware.Domain.Repositories.EntityFrameworkCore;
using SmartSoftware.EntityFrameworkCore;
using SmartSoftware.TestApp.Domain;

namespace SmartSoftware.TestApp.EntityFrameworkCore;

public class PersonRepository : EfCoreRepository<TestAppDbContext, Person, Guid>, IPersonRepository
{
    public PersonRepository(IDbContextProvider<TestAppDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public async Task<PersonView> GetViewAsync(string name)
    {
        return await (await GetDbContextAsync()).PersonView.Where(x => x.Name == name).FirstOrDefaultAsync();
    }
}
