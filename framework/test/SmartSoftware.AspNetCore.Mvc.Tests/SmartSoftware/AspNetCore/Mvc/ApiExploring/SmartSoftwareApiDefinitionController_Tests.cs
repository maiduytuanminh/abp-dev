using System.Collections.Generic;
using System.Threading.Tasks;
using Shouldly;
using SmartSoftware.Http.Modeling;
using Xunit;

namespace SmartSoftware.AspNetCore.Mvc.ApiExploring;

public class SmartSoftwareApiDefinitionController_Tests : AspNetCoreMvcTestBase
{
    [Fact]
    public async Task GetAsync()
    {
        var model = await GetResponseAsObjectAsync<ApplicationApiDescriptionModel>("/api/ss/api-definition");
        model.ShouldNotBeNull();
        model.Types.IsNullOrEmpty().ShouldBeTrue();
    }

    [Fact]
    public async Task GetAsync_IncludeTypes()
    {
        var model = await GetResponseAsObjectAsync<ApplicationApiDescriptionModel>("/api/ss/api-definition?includeTypes=true");
        model.ShouldNotBeNull();
        model.Types.IsNullOrEmpty().ShouldBeFalse();
    }
}
