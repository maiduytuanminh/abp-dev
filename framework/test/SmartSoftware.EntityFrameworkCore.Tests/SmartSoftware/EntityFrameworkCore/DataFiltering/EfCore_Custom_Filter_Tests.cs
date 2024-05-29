using System;
using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.Data;
using SmartSoftware.Domain.Repositories;
using SmartSoftware.TestApp.Domain;
using SmartSoftware.TestApp.Testing;
using Xunit;

namespace SmartSoftware.EntityFrameworkCore.DataFiltering;

public class EfCore_Custom_Filter_Tests : TestAppTestBase<SmartSoftwareEntityFrameworkCoreTestModule>
{
    private readonly IBasicRepository<Category, Guid> _categoryRepository;

    public EfCore_Custom_Filter_Tests()
    {
        _categoryRepository = GetRequiredService<IBasicRepository<Category, Guid>>();
    }

    [Fact]
    public async Task Should_Combine_SmartSoftware_And_Custom_QueryFilter_Test()
    {
        var categories = await _categoryRepository.GetListAsync();
        categories.Count.ShouldBe(1);
        categories[0].Name.ShouldBe("ss.cli");

        using (GetRequiredService<IDataFilter<ISoftDelete>>().Disable())
        {
            categories = await _categoryRepository.GetListAsync();
            categories.Count.ShouldBe(2);
            categories.ShouldContain(x => x.Name == "ss.cli" && x.IsDeleted == false);
            categories.ShouldContain(x => x.Name == "ss.core" && x.IsDeleted == true);
        }
    }
}
