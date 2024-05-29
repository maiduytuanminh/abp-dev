using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Shouldly;
using SmartSoftware.Domain.Entities.Events.Distributed;
using SmartSoftware.EventBus.Distributed;
using SmartSoftware.Testing.Utils;
using SmartSoftware.Uow;
using SmartSoftware.Users;
using Xunit;

namespace SmartSoftware.Identity;

public class Distributed_User_Change_Event_Tests : SmartSoftwareIdentityDomainTestBase
{
    private readonly IIdentityUserRepository _userRepository;
    private readonly ILookupNormalizer _lookupNormalizer;
    private readonly IdentityUserManager _userManager;
    private readonly IUnitOfWorkManager _unitOfWorkManager;
    private readonly ITestCounter _testCounter;

    public Distributed_User_Change_Event_Tests()
    {
        _userRepository = GetRequiredService<IIdentityUserRepository>();
        _userManager = GetRequiredService<IdentityUserManager>();
        _lookupNormalizer = GetRequiredService<ILookupNormalizer>();
        _unitOfWorkManager = GetRequiredService<IUnitOfWorkManager>();
        _testCounter = GetRequiredService<ITestCounter>();
    }

    [Fact]
    public void Should_Register_Handler()
    {
        GetRequiredService<IOptions<SmartSoftwareDistributedEntityEventOptions>>()
            .Value
            .EtoMappings
            .ShouldContain(m => m.Key == typeof(IdentityUser) && m.Value.EtoType == typeof(UserEto));

        GetRequiredService<IOptions<SmartSoftwareDistributedEventBusOptions>>()
            .Value
            .Handlers
            .ShouldContain(h => h == typeof(DistributedUserUpdateHandler));
    }

    [Fact]
    public async Task Should_Trigger_Distributed_EntityUpdated_Event()
    {
        _testCounter.ResetCount("EntityUpdatedEto<UserEto>");
        using (var uow = _unitOfWorkManager.Begin())
        {
            _testCounter.GetValue("EntityUpdatedEto<UserEto>").ShouldBe(0);

            var user = await _userRepository.FindByNormalizedUserNameAsync(_lookupNormalizer.NormalizeName("john.nash"));
            await _userManager.SetEmailAsync(user, "john.nash_UPDATED@smartsoftware.io");

            _testCounter.GetValue("EntityUpdatedEto<UserEto>").ShouldBe(0);
            await uow.CompleteAsync();
        }

        _testCounter.GetValue("EntityUpdatedEto<UserEto>").ShouldBe(1);
    }
}
