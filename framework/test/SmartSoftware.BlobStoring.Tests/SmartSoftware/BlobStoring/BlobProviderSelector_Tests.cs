using Shouldly;
using SmartSoftware.BlobStoring.Fakes;
using SmartSoftware.BlobStoring.TestObjects;
using SmartSoftware.DynamicProxy;
using Xunit;

namespace SmartSoftware.BlobStoring;

public class BlobProviderSelector_Tests : SmartSoftwareBlobStoringTestBase
{
    private readonly IBlobProviderSelector _selector;

    public BlobProviderSelector_Tests()
    {
        _selector = GetRequiredService<IBlobProviderSelector>();
    }

    [Fact]
    public void Should_Select_Default_Provider_If_Not_Configured()
    {
        _selector.Get<TestContainer3>().ShouldBeAssignableTo<FakeBlobProvider1>();
    }

    [Fact]
    public void Should_Select_Configured_Provider()
    {
        _selector.Get<TestContainer1>().ShouldBeAssignableTo<FakeBlobProvider1>();
        _selector.Get<TestContainer2>().ShouldBeAssignableTo<FakeBlobProvider2>();
    }
}
