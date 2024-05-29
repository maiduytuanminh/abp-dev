using System;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using Shouldly;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.Identity;
using Xunit;

namespace MyCompanyName.MyProjectName.MongoDB.Samples;

/* This is just an example test class.
 * Normally, you don't test SS framework code
 * (like default AppUser repository IRepository<AppUser, Guid> here).
 * Only test your custom repository methods.
 */
[Collection(MyProjectNameTestConsts.CollectionDefinitionName)]
public class SampleRepositoryTests : MyProjectNameMongoDbTestBase
{
    private readonly IRepository<IdentityUser, Guid> _appUserRepository;

    public SampleRepositoryTests()
    {
        _appUserRepository = GetRequiredService<IRepository<IdentityUser, Guid>>();
    }

    [Fact]
    public async Task Should_Query_AppUser()
    {
        /* Need to manually start Unit Of Work because
         * FirstOrDefaultAsync should be executed while db connection / context is available.
         */
        await WithUnitOfWorkAsync(async () =>
        {
                //Act
                var adminUser = await (await _appUserRepository.GetMongoQueryableAsync())
                .FirstOrDefaultAsync(u => u.UserName == "admin");

                //Assert
                adminUser.ShouldNotBeNull();
        });
    }
}
