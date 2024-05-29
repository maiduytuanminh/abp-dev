using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SmartSoftware.DependencyInjection;
using SmartSoftware.Guids;
using SmartSoftware.Identity;
using IdentityUser = SmartSoftware.Identity.IdentityUser;

namespace SmartSoftware.Account;

public class SmartSoftwareAccountTestDataBuilder : ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly IIdentityUserRepository _userRepository;
    private readonly AccountTestData _testData;

    public SmartSoftwareAccountTestDataBuilder(
        AccountTestData testData,
        IGuidGenerator guidGenerator,
        IIdentityUserRepository userRepository)
    {
        _testData = testData;
        _guidGenerator = guidGenerator;
        _userRepository = userRepository;
    }

    public async Task Build()
    {
        await AddUsers();
    }

    private async Task AddUsers()
    {
        var john = new IdentityUser(_testData.UserJohnId, "john.nash", "john.nash@smartsoftware.io");
        john.AddLogin(new UserLoginInfo("github", "john", "John Nash"));
        john.AddLogin(new UserLoginInfo("twitter", "johnx", "John Nash"));
        john.AddClaim(_guidGenerator, new Claim("TestClaimType", "42"));
        john.SetToken("test-provider", "test-name", "test-value");
        await _userRepository.InsertAsync(john);
    }
}
