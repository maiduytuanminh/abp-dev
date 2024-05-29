using Shouldly;
using SmartSoftware.Testing;
using Xunit;

namespace SmartSoftware.Security.Encryption;

public class StringEncryptionService_Tests : SmartSoftwareIntegratedTest<SmartSoftwareSecurityTestModule>
{
    private readonly IStringEncryptionService _stringEncryptionService;

    public StringEncryptionService_Tests()
    {
        _stringEncryptionService = GetRequiredService<IStringEncryptionService>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("This is a plain text!")]
    public void Should_Enrypt_And_Decrpyt_With_Default_Options(string plainText)
    {
        _stringEncryptionService
            .Decrypt(_stringEncryptionService.Encrypt(plainText))
            .ShouldBe(plainText);
    }
}
