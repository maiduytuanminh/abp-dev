using Shouldly;
using Xunit;

namespace SmartSoftware.SettingManagement;

public class SettingCacheItem_Tests
{
    [Fact]
    public void GetSettingNameFormCacheKeyOrNull()
    {
        var key = SettingCacheItem.CalculateCacheKey("aaa", "bbb", "ccc");
        SettingCacheItem.GetSettingNameFormCacheKeyOrNull(key).ShouldBe("aaa");
        SettingCacheItem.GetSettingNameFormCacheKeyOrNull("aaabbbccc").ShouldBeNull();
    }
}
